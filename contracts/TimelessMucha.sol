// SPDX-License-Identifier: MIT
pragma solidity ^0.8.4;

import "hardhat/console.sol";

import "./interfaces/ITimelessMucha.sol";
import "./libraries/ERC721A.sol";
import "@openzeppelin/contracts/access/Ownable.sol";
import "@openzeppelin/contracts/token/common/ERC2981.sol";
import "@openzeppelin/contracts/utils/structs/BitMaps.sol";
import "@openzeppelin/contracts/utils/cryptography/ECDSA.sol";
import "@openzeppelin/contracts/utils/math/SafeMath.sol";
import "@openzeppelin/contracts/utils/structs/EnumerableSet.sol";

error TreasuryNotSet();
error ExceedMaxGenesisPapers();
error ExceedMaxTierGenesisPapers();
error ExceedMaxPrintableAount();
error NotStarted();
error Ended();
error Inactive();
error DuringSales();
error CallerIsNotUser();
error InvalidSignature();
error ZeroQuantity();
error MintTooManyInOneTx();
error NotEnoughQuota();
error NotEnoughETH();
error NotTokenOwner();
error AlreadyPrinted();
error NotAuthorized();
error InvalidInput();
error ZeroAddress();
error InvalidToken();
error TokenNotExist();

contract TimelessMuchaStorage is IPaper {
    mapping(address => uint256) public whitelistMinted;
    mapping(uint256 => PaperInfo) public PaperItem;
    mapping(uint256 => uint256) public artworkPrintedAmount;
    mapping(uint256 => uint256) public editionLimitAmount;
    mapping(address => bool) public isAuthorized;
    mapping(address => bool) public isSigner;
    BitMaps.BitMap internal isTokenInvalid;

    uint256 public MAX_GENESISPAPERS_AMOUNT;
    uint256 public MAX_GENESISPAPERS_TIERAMOUNT;
    uint256 public MAX_GENESISPAPERS_PERTX;
    uint256 public GENESISPAPERS_PRICE;

    uint256 public genesisPapersAuctionSaleTimeStep;
    uint256 public genesisPapersAuctionSaleStartPrice;
    uint256 public genesisPapersAuctionSaleEndPrice;
    uint256 public genesisPapersAuctionSalePriceStep;
    uint256 public genesisPapersAuctionSaleMaxStepAmount;

    uint256 public whitelistMintStartTimestamp;
    uint256 public whitelistMintEndTimestamp;
    uint256 public publicMintStartTimestamp;
    uint256 public publicMintEndTimestamp;
    uint256 public dutchAuctionMintStartTimestamp;
    uint256 public dutchAuctionMintEndTimestamp;
    uint256 public printStartTimestamp;
    uint256 public printEndTimestamp;

    bool public whitelistMintStatus;
    bool public publicMintStatus;
    bool public dutchAuctionMintStatus;
    bool public printPhaseStatus;

    address public treasury;

    string public baseURI;
}

contract TimelessMucha is ITimelessMucha, IPaper, TimelessMuchaStorage, ERC721A, Ownable, ERC2981 {

    using SafeMath for uint256;
    using Strings for uint256;
    using BitMaps for BitMaps.BitMap;

      ////////////////////
     // Configurations //
    ////////////////////

    constructor()
        ERC721A("TimelessMucha", "TM") 
    {
        baseURI = "https://api.timelessmucha.xyz/metadata/";
        isSigner[owner()] = true;
        MAX_GENESISPAPERS_AMOUNT = 1860;
        MAX_GENESISPAPERS_TIERAMOUNT = 1860;
        MAX_GENESISPAPERS_PERTX = 1860;
        GENESISPAPERS_PRICE = 0.01 ether;
        genesisPapersAuctionSaleTimeStep = 120;
        genesisPapersAuctionSaleStartPrice = 0.1 ether;
        genesisPapersAuctionSaleEndPrice = 0.01 ether;
        genesisPapersAuctionSalePriceStep = 0.01 ether;
        genesisPapersAuctionSaleMaxStepAmount = 9;
        whitelistMintStartTimestamp = block.timestamp;
        whitelistMintEndTimestamp = block.timestamp + 2 hours;
        publicMintStartTimestamp = block.timestamp;
        publicMintEndTimestamp = block.timestamp + 2 hours;
        dutchAuctionMintStartTimestamp = block.timestamp;
        dutchAuctionMintEndTimestamp = block.timestamp + 2 hours;
        printStartTimestamp = block.timestamp;
        printEndTimestamp = block.timestamp + 2 hours;
        whitelistMintStatus = false;
        publicMintStatus = false;
        dutchAuctionMintStatus = false;
        printPhaseStatus = false;
        treasury = owner();
        _setDefaultRoyalty(owner(), 1000);
    }

      ///////////////
     // Modifiers //
    ///////////////

    modifier onlyAuthorized() {
        if (!isAuthorized[msg.sender] &&
            msg.sender != owner()) {
            revert NotAuthorized();
        }
        _;
    }

    modifier whitelistMintActive() {
        if (block.timestamp <= whitelistMintStartTimestamp) {
            revert NotStarted();
        }
        if (block.timestamp >= whitelistMintEndTimestamp) {
            revert Ended();
        }
        if (whitelistMintStatus == false) {
            revert Inactive();
        }
        _;
    }

    modifier publicMintActive() {
        if (block.timestamp <= publicMintStartTimestamp) {
            revert NotStarted();
        }
        if (block.timestamp >= publicMintEndTimestamp) {
            revert Ended();
        }
        if (publicMintStatus == false) {
            revert Inactive();
        }
        _;
    }

    modifier dutchAuctionMintActive() {
        if (block.timestamp <= dutchAuctionMintStartTimestamp) {
            revert NotStarted();
        }
        if (block.timestamp >= dutchAuctionMintEndTimestamp) {
            revert Ended();
        }
        if (dutchAuctionMintStatus == false) {
            revert Inactive();
        }
        _;
    }

    modifier printPhaseActive() {
        if (block.timestamp <= printStartTimestamp) {
            revert NotStarted();
        }
        if (block.timestamp >= printEndTimestamp) {
            revert Ended();
        }
        if (printPhaseStatus == false) {
            revert Inactive();
        }
        _;
    }

    modifier callerIsUser() {
        if (tx.origin != msg.sender) {
            revert CallerIsNotUser();
        }
        _;
    }

    /** @dev Override same interface function in different inheritance.
     * @param interfaceId Id of an interface to check whether the contract support
     */
    function supportsInterface(bytes4 interfaceId)
        public
        view
        override(ERC721A, ERC2981)
        returns (bool)
    {
        return super.supportsInterface(interfaceId);
    }

      //////////////////////////////
     // User Execution Functions //
    //////////////////////////////

    /** @dev Mint designated amount of genesis paper tokens to an address as owner
     * @param to Address to transfer the tokens
     * @param quantities Designated amount of tokens
     */
    function mintGiveawayGenesisPapers(
        address to, 
        uint256 quantities
    )
        external
        override
        onlyAuthorized
    {
        if (totalSupply().add(quantities) > MAX_GENESISPAPERS_AMOUNT) {
            revert ExceedMaxGenesisPapers();
        }

        _safeMint(to, quantities);
        emit MintGenesisPaper(to, quantities, totalSupply());
    }

    /** @dev Mint genesis paper tokens as Whitelisted Address
     * @param quantities Amount of genesis paper tokens the address wants to mint
     * @param maxQuantites Maximum amount of genesis paper tokens the address can mint
     * @param signature Signature used to verify the minter address and amount of claimable tokens
     */
    function mintWhitelistGenesisPapers(
        uint256 quantities, 
        uint256 maxQuantites, 
        bytes calldata signature
    )
        external
        payable
        override
        whitelistMintActive
    {
        bytes32 hash = ECDSA.toEthSignedMessageHash(
            keccak256(
                abi.encodePacked(
                    msg.sender,
                    maxQuantites
                )
            )
        );

        if (!isSigner[ECDSA.recover(hash, signature)]) {
            revert InvalidSignature();
        }

        if (totalSupply() + quantities > MAX_GENESISPAPERS_TIERAMOUNT) {
            revert ExceedMaxTierGenesisPapers();
        }

        if (totalSupply() + quantities > MAX_GENESISPAPERS_AMOUNT) {
            revert ExceedMaxGenesisPapers();
        }

        if (quantities == 0) {
            revert ZeroQuantity();
        }

        if (msg.value < quantities.mul(GENESISPAPERS_PRICE)) {
            revert NotEnoughETH();
        }

        if (whitelistMinted[msg.sender] + quantities > maxQuantites) {
            revert NotEnoughQuota();
        }

        whitelistMinted[msg.sender] += quantities;

        _safeMint(msg.sender, quantities);
        emit MintGenesisPaper(msg.sender, quantities, totalSupply());
    }

    /** @dev Mint genesis paper tokens during public sale
     * @param quantities Amount of genesis paper tokens the address wants to mint
     */
    function mintPublicGenesisPapers(uint256 quantities)
        external
        payable
        override
        publicMintActive
        callerIsUser
    {

        if (totalSupply() + quantities > MAX_GENESISPAPERS_TIERAMOUNT) {
            revert ExceedMaxTierGenesisPapers();
        }

        if (totalSupply() + quantities > MAX_GENESISPAPERS_AMOUNT) {
            revert ExceedMaxGenesisPapers();
        }

        if (quantities == 0) {
            revert ZeroQuantity();
        }

        if (msg.value < quantities.mul(GENESISPAPERS_PRICE)) {
            revert NotEnoughETH();
        }

        if (quantities > MAX_GENESISPAPERS_PERTX) {
            revert MintTooManyInOneTx();
        }

        _safeMint(msg.sender, quantities);
        emit MintGenesisPaper(msg.sender, quantities, totalSupply());
    }

    /** @dev Mint genesis paper tokens during dutch auction sale
     * @param quantities Amount of genesis paper tokens the address wants to mint
     */
    function mintDutchAuctionGenesisPapers(uint256 quantities)
        external 
        payable
        override
        dutchAuctionMintActive
        callerIsUser
    {
        if (totalSupply() + quantities > MAX_GENESISPAPERS_TIERAMOUNT) {
            revert ExceedMaxTierGenesisPapers();
        }

        if (totalSupply() + quantities > MAX_GENESISPAPERS_AMOUNT) {
            revert ExceedMaxGenesisPapers();
        }

        if (quantities == 0) {
            revert ZeroQuantity();
        }

        if (msg.value < quantities.mul(GENESISPAPERS_PRICE)) {
            revert NotEnoughETH();
        }

        if (quantities > MAX_GENESISPAPERS_PERTX) {
            revert MintTooManyInOneTx();
        }

        _safeMint(msg.sender, quantities);
        emit MintGenesisPaper(msg.sender, quantities, totalSupply());
    }

    /** @dev Mint genesis paper tokens during dutch auction sale
     * @param tokenId Amount of genesis paper tokens the address wants to mint
     * @param artworkId test
     */
    function printGenesisPapers(
        uint256 tokenId, 
        uint256 artworkId
    )
        external
        override
        printPhaseActive
    {
        if (ownerOf(tokenId) != msg.sender) {
            revert NotTokenOwner();
        }

        if (artworkPrintedAmount[artworkId] + 1 > editionLimitAmount[artworkId]) {
            revert ExceedMaxPrintableAount();
        }

        if (PaperItem[tokenId].printed == true) {
            revert AlreadyPrinted();
        }

        artworkPrintedAmount[artworkId] += 1;
        PaperItem[tokenId] = PaperInfo(artworkId, artworkPrintedAmount[artworkId], true);
        emit PrintGenesisPaper(tokenId);
    }

      ////////////////////////////
     // Info Getters Functions //
    ////////////////////////////

    /** @dev Retrieve all tokenIds of a given address
     * @param owner Address which caller wants to get all of its tokenIds
     */
    function tokensOfOwner(address owner)
        public 
        view 
        override
        returns(uint256[] memory tokenIds)
    {
        uint256 tokenCount = balanceOf(owner);
        if (tokenCount == 0) {
            // Return an empty array
            return new uint256[](0);
        } else {
            uint256[] memory result = new uint256[](tokenCount);
            for (uint256 index = 0; index < tokenCount; index++) {
                result[index] = tokenOfOwnerByIndex(owner, index);
            }
            return result;
        }
    }

    /** @dev Retrieve the current price of dutch aution sale
     */
    function getDutchAuctionPrice()
        public 
        view
        override
        returns (uint256 price)
    {
        if (block.timestamp < dutchAuctionMintStartTimestamp ||
            block.timestamp > dutchAuctionMintEndTimestamp ||
            !dutchAuctionMintStatus
        ) {
            return genesisPapersAuctionSaleStartPrice;
        } else {
            uint256 curStep = (block.timestamp.sub(dutchAuctionMintStartTimestamp)).div(genesisPapersAuctionSaleTimeStep);

            return curStep < genesisPapersAuctionSaleMaxStepAmount ?
                genesisPapersAuctionSaleStartPrice.sub(curStep.mul(genesisPapersAuctionSalePriceStep)) :
                genesisPapersAuctionSaleEndPrice;
        }
    }

    /** @dev Retrieve the print status according to tokenId
     * @param tokenId TokenId which caller wants to get its print status
     */
    function getTokenPrintStatus(uint256 tokenId)
        external 
        view 
        override
        returns(bool status)
    {
        return PaperItem[tokenId].printed;
    }

    /** @dev Retrieve all the print status of a given address
     * @param owner Address which caller wants to get all of its print status
     */
    function getTokenPrintStatusByOwner(address owner)
        external 
        view
        override
        returns(bool[] memory status)
    {
        uint256 tokenCount = balanceOf(owner);
        if (tokenCount == 0) {
            // Return an empty array
            return new bool[](0);
        } else {
            bool[] memory result = new bool[](tokenCount);
            for (uint256 index = 0; index < tokenCount; index++) {
                uint256 tokenId = tokenOfOwnerByIndex(owner, index);
                result[index] = PaperItem[tokenId].printed;
            }
            return result;
        }
    }

    /** @dev Retrieve the status of whether a token is set to invalid
     * @param tokenId TokenId which caller wants to get its valid status
     */
    function getTokenValidStatus(uint256 tokenId)
        external 
        view 
        override
        returns(bool status)
    {
        return isTokenInvalid.get(tokenId);
    }

    /** @dev Retrieve all the the status of whether a token is set to invalid of a giving address.
     * @param owner Address which caller wants to get all of its token valid status
     */
    function getTokenValidStatusByOwner(address owner)
        external 
        view
        override
        returns(bool[] memory status)
    {
        uint256 tokenCount = balanceOf(owner);
        if (tokenCount == 0) {
            return new bool[](0);
        } else {
            bool[] memory result = new bool[](tokenCount);
            for (uint256 index = 0; index < tokenCount; index++) {
                uint256 tokenId = tokenOfOwnerByIndex(owner, index);
                result[index] = isTokenInvalid.get(tokenId);
            }
            return result;
        }
    }

    /** @dev Get all the holders address, work as snapshots
     */
    function getAllHolders()
        public 
        view
        override 
        returns(address[] memory holdersList)
    {
        uint256 ownersCount = 0;
        uint256 tokenCount = totalSupply();
        bool[] memory tokenCheck = new bool[](tokenCount);
        if (tokenCount == 0) {
            return new address[](0);
        } else {
            address[] memory temp = new address[](tokenCount);
            for (uint256 index = 0; index < tokenCount; index++) {
                if (tokenCheck[index]==false) {
                    address owner = ownerOf(index);
                    uint256 ownerBalance = balanceOf(owner);
                    uint256[] memory tokenIds = tokensOfOwner(owner);
                    for (uint256 idx = 0; idx < ownerBalance; idx++) {
                        tokenCheck[tokenIds[idx]] = true;
                    }
                    temp[ownersCount] = owner;
                    ownersCount++;
                }
            }
            address[] memory result = new address[](ownersCount);
            for (uint256 index = 0; index < ownersCount; index++) {
                result[index] = temp[index];
            }
            return result;
        }
    }

    /** @dev Get all the holders address and their related balance, work as snapshots
     */
    function getAllHoldersInfo() 
        external 
        view
        override 
        returns(HolderInfo[] memory holdersList)
    {
        uint256 ownersCount = 0;
        uint256 tokenCount = totalSupply();
        bool[] memory tokenCheck = new bool[](tokenCount);
        if (tokenCount == 0) {
            return new HolderInfo[](0);
        } else {
            HolderInfo[] memory temp = new HolderInfo[](tokenCount);
            for (uint256 index = 0; index < tokenCount; index++) {
                if (tokenCheck[index]==false) {
                    address owner = ownerOf(index);
                    uint256 ownerBalance = balanceOf(owner);
                    uint256[] memory tokenIds = tokensOfOwner(owner);
                    for (uint256 idx = 0; idx < ownerBalance; idx++) {
                        tokenCheck[tokenIds[idx]] = true;
                    }
                    temp[ownersCount] = HolderInfo(owner, ownerBalance, tokenIds);
                    ownersCount++;
                }
            }
            HolderInfo[] memory result = new HolderInfo[](ownersCount);
            for (uint256 index = 0; index < ownersCount; index++) {
                result[index] = temp[index];
            }
            return result;
        }
    }

    /** @dev Retrieve token URI to get the metadata of a token
     * @param tokenId TokenId which caller wants to get the metadata of
     */
	function tokenURI(uint256 tokenId) 
        public 
        view 
        override 
        returns (string memory curTokenURI) 
    {
        if(!_exists(tokenId)) {
            revert TokenNotExist();
        }
		return string(abi.encodePacked(baseURI, tokenId.toString()));
	}

      /////////////////////////
     // Set Phase Functions //
    /////////////////////////

    /** @dev Set the status, starting time, and ending time of the whitelist mint phase
     * @param newWhitelistMintStartTimestamp After this timestamp the whitelist mint phase will be enabled
     * @param newWhitelistMintEndTimestamp After this timestamp the whitelist mint phase will be disabled
     * @param newWhitelistMintEnableStatus True if the whitelist mint phase have started, otherwise false
     * @notice Start time must be smaller than end time
     */
    function setWhitelistMintPhase(
        uint256 newWhitelistMintStartTimestamp, 
        uint256 newWhitelistMintEndTimestamp, 
        bool newWhitelistMintEnableStatus
    )
        external
        override
        onlyAuthorized
    {
        if(newWhitelistMintStartTimestamp > newWhitelistMintEndTimestamp) {
            revert InvalidInput();
        }
        whitelistMintStartTimestamp = newWhitelistMintStartTimestamp;
        whitelistMintEndTimestamp = newWhitelistMintEndTimestamp;
        whitelistMintStatus = newWhitelistMintEnableStatus;
    }

    /** @dev Set the status, starting time, and ending time of the public mint phase
     * @param newPublicMintStartTimestamp After this timestamp the public mint phase will be enabled
     * @param newPublicMintEndTimestamp After this timestamp the public mint phase will be disabled
     * @param newPublicMintEnableStatus True if the public mint phase have started, otherwise false
     * @notice Start time must be smaller than end time
     */
    function setPublicMintPhase(
        uint256 newPublicMintStartTimestamp, 
        uint256 newPublicMintEndTimestamp, 
        bool newPublicMintEnableStatus
    )
        external
        override
        onlyAuthorized
    {        
        if(newPublicMintStartTimestamp > newPublicMintEndTimestamp) {
            revert InvalidInput();
        }
        publicMintStartTimestamp = newPublicMintStartTimestamp;
        publicMintEndTimestamp = newPublicMintEndTimestamp;
        publicMintStatus = newPublicMintEnableStatus;
    }

    /** @dev Set the status, starting time, and ending time of the dutch aution mint phase
     * @param newDutchAuctionMintStartTimestamp After this timestamp the dutch aution mint phase will be enabled
     * @param newDutchAuctionMintEndTimestamp After this timestamp the dutch aution mint phase will be disabled
     * @param newDutchAuctionMintStatus True if the dutch aution mint phase have started, otherwise false
     * @notice Start time must be smaller than end time
     */
    function setDutchAuctionMintPhase(
        uint256 newDutchAuctionMintStartTimestamp, 
        uint256 newDutchAuctionMintEndTimestamp, 
        bool newDutchAuctionMintStatus
    )
        external
        override
        onlyAuthorized
    {
        if(newDutchAuctionMintStartTimestamp > newDutchAuctionMintEndTimestamp) {
            revert InvalidInput();
        }
        dutchAuctionMintStartTimestamp = newDutchAuctionMintStartTimestamp;
        dutchAuctionMintEndTimestamp = newDutchAuctionMintEndTimestamp;
        dutchAuctionMintStatus = newDutchAuctionMintStatus;
    }

    /** @dev Set the sales-related information of the dutch aution mint phase
     * @param newDutchAuctionTimestep Timestep of the frequency to adjust the price of the dutch auction sales
     * @param newDutchAuctionStartPrice Initial price of the dutch auction sales
     * @param newDutchAuctionEndPrice Final price of the dutch auction sales
     * @param newDutchAuctionPriceStep Pricestep of the amount to adjust the price of the dutch auction sales during each timestep
     * @param newDutchAuctionMaxStepAmount Maximum number of the times to adjust the price
     * @notice Start price must be smaller than end price
     * @notice Start price must be equal to Max Step * Price Step + End Price
     * @notice Can't adjust sales-related information price during dutch aution sales
     */
    function setDutchAuctionSaleInfo(
        uint256 newDutchAuctionTimestep, 
        uint256 newDutchAuctionStartPrice, 
        uint256 newDutchAuctionEndPrice, 
        uint256 newDutchAuctionPriceStep, 
        uint256 newDutchAuctionMaxStepAmount
    ) 
        external
        override
        onlyOwner
    {
        if (newDutchAuctionStartPrice < newDutchAuctionEndPrice) {
            revert InvalidInput();
        }
        if (newDutchAuctionStartPrice != newDutchAuctionEndPrice.add(newDutchAuctionMaxStepAmount.mul(newDutchAuctionPriceStep))) {
            revert InvalidInput();
        }
        if (
            dutchAuctionMintStatus &&
            block.timestamp >= dutchAuctionMintStartTimestamp &&
            block.timestamp <= dutchAuctionMintEndTimestamp
        ) {
            revert DuringSales();
        }
        genesisPapersAuctionSaleTimeStep = newDutchAuctionTimestep;
        genesisPapersAuctionSaleStartPrice = newDutchAuctionStartPrice;
        genesisPapersAuctionSaleEndPrice = newDutchAuctionEndPrice;
        genesisPapersAuctionSalePriceStep = newDutchAuctionPriceStep;
        genesisPapersAuctionSaleMaxStepAmount = newDutchAuctionMaxStepAmount;
    }

    /** @dev Set the status, starting time, and ending time of the print phase
     * @param newPrintStartTimestamp After this timestamp the print phase will be enabled
     * @param newPrintEndTimestamp After this timestamp the print phase will be disabled
     * @param newPrintEnableStatus True if the print phase have started, otherwise false
     * @notice Start time must be smaller than end time
     */
    function setPrintPhase(
        uint256 newPrintStartTimestamp, 
        uint256 newPrintEndTimestamp, 
        bool newPrintEnableStatus
    ) 
        external
        override
        onlyAuthorized
    {
        if(newPrintStartTimestamp > newPrintEndTimestamp) {
            revert InvalidInput();
        }
        printStartTimestamp = newPrintStartTimestamp;
        printEndTimestamp = newPrintEndTimestamp;
        printPhaseStatus = newPrintEnableStatus;
    }

      ////////////////////////////////////////
     // Set Roles & Token Status Functions //
    ////////////////////////////////////////

    /** @dev Set the status of whether an address is authorized
     * @param authorizedAddress Address to change its authorized status
     * @param newAuthorizedStatus New status to assign to the authorizedAddress
     */
    function setAuthorized(
        address authorizedAddress, 
        bool newAuthorizedStatus
    )
        external
        override
        onlyOwner
    {
        isAuthorized[authorizedAddress] = newAuthorizedStatus;
    }

    /** @dev Set the status of whether an address is signer
     * @param signerAddress Address to change its status as a signer
     * @param newSignerStatus New status to assign to the signerAddress
     */
    function setSigner(
        address signerAddress,
        bool newSignerStatus
    )
        external
        override
        onlyOwner
    {
        isSigner[signerAddress] = newSignerStatus;
    }

    /** @dev Set the status of whether an address is authorized in batch
     * @param authorizedAddressArray Address list to change its authorized status
     * @param newAuthorizedStatusArray New status list to assign to the authorizedAddress
     */
    function setAuthorizedInBatch(
        address[] memory authorizedAddressArray, 
        bool[] memory newAuthorizedStatusArray
    )
        external
        override
        onlyOwner
    {
        if(authorizedAddressArray.length != newAuthorizedStatusArray.length) {
            revert InvalidInput();
        }
        uint256 listLegnth = authorizedAddressArray.length;
        for(uint256 index = 0; index < listLegnth; index++) {
            isAuthorized[authorizedAddressArray[index]] = newAuthorizedStatusArray[index];
        }
    }

    /** @dev Set the status of whether an address is signer in batch
     * @param signerAddressArray Address list to change its status as a signer
     * @param newSignerStatusArray New status list to assign to the signerAddress
     */
    function setSignerInBatch(
        address[] memory signerAddressArray, 
        bool[] memory newSignerStatusArray
    )
        external
        override
        onlyOwner
    {
        if(signerAddressArray.length != newSignerStatusArray.length) {
            revert InvalidInput();
        }
        uint256 listLegnth = signerAddressArray.length;
        for(uint256 index = 0; index < listLegnth; index++) {
            isSigner[signerAddressArray[index]] = newSignerStatusArray[index];
        }
    }

    /** @dev Set the specific token to invalid, to revert the transfering transaction
     * @param tokenId Token Id that owner wants to set to invalid
     */
    function setTokenInvalid(uint256 tokenId)
        external
        override
        onlyOwner
    {
        isTokenInvalid.set(tokenId);
    }

    /** @dev Set the specific tokens to invalid in batch, to revert the transfering transactions
     * @param tokenIds Token Id list that owner wants to set to invalid
     */
    function setTokenInvalidInBatch(uint256[] memory tokenIds)
        external
        override
        onlyOwner
    {
        uint256 listLegnth = tokenIds.length;
        for(uint256 index = 0; index < listLegnth; index++) {
            isTokenInvalid.set(tokenIds[index]);
        }
    }

    /** @dev Set the specific token to valid, to revert the transfering transaction 
     * @param tokenId Token Id that owner wants to set to valid
     */
    function setTokenValid(uint256 tokenId)
        external
        override
        onlyOwner
    {
        isTokenInvalid.unset(tokenId);
    }

    /** @dev Set the specific token to valid in batcg, to revert the transfering transactions 
     * @param tokenIds Token Id that owner wants to set to invalid
     */
    function setTokenValidInBatch(uint256[] memory tokenIds)
        external
        override
        onlyOwner
    {
        uint256 listLegnth = tokenIds.length;
        for(uint256 index = 0; index < listLegnth; index++) {
            isTokenInvalid.unset(tokenIds[index]);
        }
    }

      //////////////////////////
     // Set Params Functions //
    //////////////////////////

    /** @dev Set the royalties information for platforms that support ERC2981, LooksRare & X2Y2
     * @param receiver Address that should receive royalties
     * @param feeNumerator Amount of royalties that collection creator wants to receive
     */
    function setDefaultRoyalty(
        address receiver, 
        uint96 feeNumerator
    )
        external
        override
        onlyOwner
    {
        _setDefaultRoyalty(receiver, feeNumerator);
    }

    /** @dev Set the royalties information for platforms that support ERC2981, LooksRare & X2Y2
     * @param receiver Address that should receive royalties
     * @param feeNumerator Amount of royalties that collection creator wants to receive
     */
    function setTokenRoyalty(
        uint256 tokenId,
        address receiver,
        uint96 feeNumerator
    ) 
        external 
        override
        onlyOwner 
    {
        _setTokenRoyalty(tokenId, receiver, feeNumerator);
    }

    /** @dev Set the URI for tokenURI, which returns the metadata of token
     * @param newBaseURI New URI that caller wants to set as tokenURI
     */
    function setBaseURI(string memory newBaseURI)
        external
        override
        onlyOwner
    {
        baseURI = newBaseURI;
    }

    /** @dev Set the maximum printable editions of each artwork
     * @param id Id of artwork that the caller want to set its maximum printable amount
     * @param maxAmount Maximum printable amount that the caller want to set for the artwork
     */
    function setArtworkEditionMaxLimit(
        uint256 id, 
        uint256 maxAmount
    )
        external
        override
        onlyOwner
    {
        editionLimitAmount[id] = maxAmount;
    }

    /** @dev Set the maximum printable editions of each artwork in batch
     * @param ids Id list of artworks that the caller want to set their maximum printable amount
     * @param maxAmounts Maximum printable amount list that the caller want to set for the artworks
     */
    function setArtworkEditionMaxLimitInBatch(
        uint256[] memory ids, 
        uint256[] memory maxAmounts
    )
        external
        override
        onlyOwner
    {
        if(ids.length != maxAmounts.length) {
            revert InvalidInput();
        }
        uint256 listLegnth = ids.length;
        for(uint256 index = 0; index < listLegnth; index++) {
            editionLimitAmount[ids[index]] = maxAmounts[index];
        }
    }

    /** @dev Set the price of the genesis paper tokens
     * @param newPrice New price of the genesis paper tokens
     */ 
    function setGenesisPapersPrice(uint256 newPrice)
        external
        override
        onlyOwner
    {
        GENESISPAPERS_PRICE = newPrice;
    }

    /** @dev Set the maximum total amount of genesis paper tokens
     * @param newMaxMuchaPapersAmount Maximum total amount of the genesis paper tokens
     */
    function setMaxGenesisPapersAmount(uint256 newMaxMuchaPapersAmount)
        external
        override
        onlyOwner
    {
        MAX_GENESISPAPERS_AMOUNT = newMaxMuchaPapersAmount;
    }

    /** @dev Set the maximum limit amount of each sales phase
     * @param newMaxMuchaPapersTierAmount Maximum limit amount of this sales phase
     */
    function setMaxGenesisPapersTierAmount(uint256 newMaxMuchaPapersTierAmount)
        external
        override
        onlyOwner
    {
        MAX_GENESISPAPERS_TIERAMOUNT = newMaxMuchaPapersTierAmount;
    }

    /** @dev Set the maximum limit amount of a single transaction
     * @param newMaxMuchaPapersPerTx Maximum limit amount of a single transaction
     */
    function setMaxGenesisPapersPerTx(uint256 newMaxMuchaPapersPerTx)
        external
        override
        onlyOwner
    {
        MAX_GENESISPAPERS_PERTX = newMaxMuchaPapersPerTx;
    }

    /** @dev Set the address that act as treasury and recieve all the fund from token contract
     * @param newTreasuryAddress New address that caller wants to set as the treasury address
     */
    function setTreasury(address newTreasuryAddress)
        external
        override
        onlyOwner
    {
        if(newTreasuryAddress == address(0)) {
            revert ZeroAddress();
        }
        treasury = newTreasuryAddress;
    }

      /////////////////////////////
     // Withdraw Fund Functions //
    /////////////////////////////

    /** @dev Retrieve fund from this contract to the treasury with the according amount
     * @param amount The amount of fund that the caller wants to retrieve
     */
    function withdraw(uint256 amount)
        external
        override
        onlyOwner
    {
        if (treasury == address(0)) {
            revert TreasuryNotSet();
        }
        payable(treasury).transfer(amount);
    }

    /** @dev Checker before token transfer
     * @param from Address to transfer the token from
     * @param to Address to recieve the token
     * @param startTokenId Init Id to start to transfer the tokens
     * @param quantity Amount of tokens that will be transferred
     * @notice If the token is set to Invalid, then the transfer will be reverted
     */
    function _beforeTokenTransfers(
        address from,
        address to,
        uint256 startTokenId,
        uint256 quantity
    ) 
        internal
        override 
    {
        if (from == address(0) || to == address(0)) {
            return;
        }

        for (
            uint256 tokenId = startTokenId;
            tokenId < startTokenId + quantity;
            ++tokenId
        ) {
            if (isTokenInvalid.get(tokenId)) {
                revert InvalidToken();
            }
        }
    }
}