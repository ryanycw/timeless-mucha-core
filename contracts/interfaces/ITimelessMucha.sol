// SPDX-License-Identifier: MIT
pragma solidity 0.8.4;

interface IPaper {
  // Struct to store metadata of each paper
  struct PaperInfo {
    uint256 artworkId;
    uint256 editionNumber;
    bool printed;
  }
}

interface ITimelessMucha {
  // Struct to return each holders balance
  struct HolderInfo {
    address holderAddress;
    uint256 balance;
    uint256[] tokenIds;
  }
  
    //////////////////////////////
   // User Execution Functions //
  //////////////////////////////

  // Mint giveaway genesis papers to an address by owner.
  function mintGiveawayGenesisPapers(address to, uint256 quantities) external;
  // Whitelisted addresses mint specific amount of tokens with signature & maximum mintable amount to verify.
  function mintWhitelistGenesisPapers(uint256 quantities, uint256 maxQuantites, bytes calldata signature) external payable;
  // Public addresses mint specific amount of tokens in public sale.
  function mintPublicGenesisPapers(uint256 quantities) external payable;
  // Public addresses mint specific amount of tokens in dutch aution sale.
  function mintDutchAuctionGenesisPapers(uint256 quantities) external payable;
  // Genesis paper owner can print their papers with the artworks they like.
  function printGenesisPapers(uint256 tokenId, uint256 artworkId) external;

    ////////////////////////////
   // Info Getters Functions //
  ////////////////////////////

  // Get the current price of the dutch auction sale.
  function getDutchAuctionPrice() external view returns (uint256 price);
  // Get true if a paper was printed, false otherwise.
  function getTokenPrintStatus(uint256 tokenId) external view returns(bool status);
  // Get all the print status of all the tokens of an address.
  function getTokenPrintStatusByOwner(address owner) external view returns(bool[] memory status);
  // Get the status of whether a token is set to invalid.
  function getTokenValidStatus(uint256 tokenId) external view returns(bool status);
  // Get all the status of whether a token is set to invalid of a owner's address.
  function getTokenValidStatusByOwner(address owner) external view returns(bool[] memory status);
  // Get all the holders of the genesis paper tokens.
  function getAllHolders() external view returns(address[] memory holdersList);
  // Get all the holders and their balance of the genesis paper tokens.
  function getAllHoldersInfo() external view returns(HolderInfo[] memory holdersList);

    /////////////////////////
   // Set Phase Functions //
  /////////////////////////

  // Set the variables to enable the whitelist mint phase by owner.
  function setWhitelistMintPhase(uint256 newWhitelistMintStartTimestamp, uint256 newWhitelistMintEndTimestamp) external;
  // Set the variables to enable the public mint phase by owner.
  function setPublicMintPhase(uint256 newPublicMintStartTimestamp, uint256 newPublicMintEndTimestamp) external;
  // Set the variables to enable the dutch auction sale phase by owner.
  function setDutchAuctionMintPhase(uint256 newDutchAuctionMintStartTimestamp, uint256 newDutchAuctionMintEndTimestamp) external;
  // Set the sale-related variables used in the dutch auction sale by owner.
  function setDutchAuctionSaleInfo(uint256 newDutchAuctionTimestep, uint256 newDutchAuctionStartPrice, uint256 newDutchAuctionEndPrice, uint256 newDutchAuctionPriceStep, uint256 newDutchAuctionMaxStepAmount) external;
  // Set the variables to enable the print phase by owner.
  function setPrintPhase(uint256 newPrintStartTimestamp, uint256 newPrintEndTimestamp) external;

    ////////////////////////////////////////
   // Set Roles & Token Status Functions //
  ////////////////////////////////////////

  // Set the authorized status of an address, true to have authorized access, false otherwise.
  function setAuthorized(address authorizedAddress, bool newAuthorizedStatus) external;
  // Set the address to generate and validate the signature.
  function setSigner(address signerAddress, bool newSignerStatus) external;
  // Set the authorized status of an address, true to have authorized access, false otherwise in batch.
  function setAuthorizedInBatch(address[] memory authorizedAddressArray, bool[] memory newAuthorizedStatusArray) external;
  // Set the address to generate and validate the signature in batch.
  function setSignerInBatch(address[] memory signerAddressArray, bool[] memory newSignerStatusArray) external;
  // Set token invalid, so that the token cannot be transferred.
  function setTokenInvalid(uint256 tokenId) external;
  // Set token invalid in batch, so that the tokens cannot be transferred.
  function setTokenInvalidInBatch(uint256[] memory tokenIds) external;
  // Set token valid, so that the token can be transferred.
  function setTokenValid(uint256 tokenId) external;
  // Set token valid in batch, so that the token can be transferred.
  function setTokenValidInBatch(uint256[] memory tokenIds) external;

    //////////////////////////
   // Set Params Functions //
  //////////////////////////

  // Set collection royalties with platforms that support ERC2981.
  function setDefaultRoyalty(address receiver, uint96 feeNumerator) external;
  // Set royalties of specific token with platforms that support ERC2981.
  function setTokenRoyalty(uint256 tokenId, address receiver, uint96 feeNumerator) external;
  // Set the URI to return the tokens metadata.
  function setBaseURI(string memory newBaseURI) external;
  // Set the maximum printable editions of each artwork.
  function setArtworkEditionMaxLimit(uint256 id, uint256 maxAmount) external;
  // Set the maximum printable editions of each artwork in batch.
  function setArtworkEditionMaxLimitInBatch(uint256[] memory ids, uint256[] memory maxAmounts) external;
  // Set the price of the genesis paper tokens.
  function setGenesisPapersPrice(uint256 newPrice) external;
  // Set the maximum total amount of genesis paper tokens.
  function setMaxGenesisPapersAmount(uint256 newMaxGenesisPapersAmount) external;
  // Set the maximum limit amount of each sales phase.
  function setMaxGenesisPapersTierAmount(uint256 newMaxGenesisPapersTierAmount) external;
  // Set the maximum limit amount of a single transaction.
  function setMaxGenesisPapersPerTx(uint256 newMaxGenesisPapersPerTx) external;
  // Set the address to transfer the contract fund to.
  function setTreasury(address newTreasuryAddress) external;

    /////////////////////////////
   // Withdraw Fund Functions //
  /////////////////////////////

  // Withdraw all the fund inside the contract to the treasury address.
  function withdraw(uint256 amount) external;
  // This event is triggered whenever a call to #mintGiveawayGenesisPapers, #mintWhitelistGenesisPapers, #mintPublicGenesisPapers, and #mintDutchAuctionGenesisPapers succeeds.
  event MintGenesisPaper(address owner, uint256 quantity, uint256 totalSupply);
  // This event is triggered whenever a call to #printGenesisPapers succeeds.
  event PrintGenesisPaper(uint256 tokenId);
  // This event is triggered whenever a call to #setWhitelistMintPhase succeeds.
  event WhitelistMintPhaseSet(uint256 newWhitelistMintStartTimestamp, uint256 newWhitelistMintEndTimestamp);
  // This event is triggered whenever a call to #setPublicMintPhase succeeds.
  event PublicMintPhaseSet(uint256 newPublicMintStartTimestamp, uint256 newPublicMintEndTimestamp);
  // This event is triggered whenever a call to #setDutchAuctionMintPhase succeeds.
  event DutchAuctionMintPhaseSet(uint256 newDutchAuctionMintStartTimestamp, uint256 newDutchAuctionMintEndTimestamp);
  // This event is triggered whenever a call to #setDutchAuctionSaleInfo succeeds.
  event DutchAuctionSaleInfoSet(uint256 newDutchAuctionTimestep, uint256 newDutchAuctionStartPrice, uint256 newDutchAuctionEndPrice, uint256 newDutchAuctionPriceStep, uint256 newDutchAuctionMaxStepAmount);
  // This event is triggered whenever a call to #setPrintPhase succeeds.
  event PrintPhaseSet(uint256 newPrintStartTimestamp, uint256 newPrintEndTimestamp);
  // This event is triggered whenever a call to #setGenesisPapersPrice succeeds.
  event GenesisPapersPriceSet(uint256 newPrice);
  // This event is triggered whenever a call to #setMaxGenesisPapersAmount succeeds.
  event GenesisPaperMaxAmountSet(uint256 newMaxGenesisPapersAmount);
  // This event is triggered whenever a call to #setMaxGenesisPapersTierAmount succeeds.
  event GenesisPaperMaxTierAmountSet(uint256 newMaxGenesisPapersTierAmount);
  // This event is triggered whenever a call to #setMaxGenesisPapersPerTx succeeds.
  event GenesisPaperMaxPerTxAmountSet(uint256 newMaxGenesisPapersPerTx);
}