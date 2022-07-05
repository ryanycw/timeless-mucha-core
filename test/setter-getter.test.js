require("dotenv/config");
const fs = require('fs');
const utils = require("../helpers/utils");
const sign = require("../helpers/signature")
const { BN, constants, expectEvent, expectRevert } = require('@openzeppelin/test-helpers');
const { expect } = require("hardhat");

describe("Timeless  Mucha", function() {
    let oneGwei, gasPrice, chainId;
    beforeEach(async function () {
        [owner, addr1, addr2, addr3, ...addrs] = await ethers.getSigners();
        chainId = await ethers.provider.getNetwork();
        gasPrice = ethers.BigNumber.from("90")
        oneGwei = ethers.BigNumber.from("1000000000");
        const genesisPaperContract = await ethers.getContractFactory("TimelessMucha", owner);
        genesisPaperToken = await genesisPaperContract.deploy();
        await genesisPaperToken.setArtworkEditionMaxLimitInBatch([...Array(40 - 1 + 1).keys()].map(x => x + 1), Array(40).fill(40));
    });

    describe("Test Set and Get Related Functions", function() {
        it("Happy Path - Set Valid & Invalid", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");

            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 0);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("9");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("1");

            await genesisPaperToken.connect(owner).setTokenInvalid(1);
            expect((await genesisPaperToken.getTokenValidStatus(1))).to.be.true;
            
            await expectRevert(
                genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 1),
                "InvalidToken()"
            );
            await genesisPaperToken.connect(owner).setTokenValid(1);
            expect((await genesisPaperToken.getTokenValidStatus(1))).to.be.false;

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 1);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("8");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("2");

            await genesisPaperToken.connect(owner).setTokenInvalidInBatch([2,3,4]);
            expect((await genesisPaperToken.getTokenValidStatus(2))).to.be.true;
            expect((await genesisPaperToken.getTokenValidStatus(3))).to.be.true;
            expect((await genesisPaperToken.getTokenValidStatus(4))).to.be.true;

            const rawValidStatus = (await genesisPaperToken.getTokenValidStatusByOwner(addr1.address)).toString();
            const validStatusList = rawValidStatus.split(',');
            expect(validStatusList[0]).to.be.equal('true');
            expect(validStatusList[1]).to.be.equal('true');
            expect(validStatusList[2]).to.be.equal('true');
            expect(validStatusList[3]).to.be.equal('false');

            await expectRevert(
                genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 2),
                "InvalidToken()"
            );
            await genesisPaperToken.connect(owner).setTokenValid(2);
            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 2);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("7");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("3");

            await genesisPaperToken.connect(owner).setTokenValidInBatch([3,4]);
            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 3);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("6");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("4");

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 4);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("5");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("5");
        });

        it("Unauthorized - Set Valid & Invalid", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");

            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 0);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("9");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("1");

            await expectRevert(
                genesisPaperToken.connect(addr1).setTokenInvalid(1),
                'Ownable: caller is not the owner'
            );

            await genesisPaperToken.connect(owner).setTokenInvalid(1);
            expect((await genesisPaperToken.getTokenValidStatus(1))).to.be.true;
            
            await expectRevert(
                genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 1),
                "InvalidToken()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setTokenValid(1),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setTokenValid(1);
            expect((await genesisPaperToken.getTokenValidStatus(1))).to.be.false;

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 1);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("8");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("2");

            await expectRevert(
                genesisPaperToken.connect(addr1).setTokenInvalidInBatch([2,3,4]),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setTokenInvalidInBatch([2,3,4]);
            expect((await genesisPaperToken.getTokenValidStatus(2))).to.be.true;
            expect((await genesisPaperToken.getTokenValidStatus(3))).to.be.true;
            expect((await genesisPaperToken.getTokenValidStatus(4))).to.be.true;

            const rawValidStatus = (await genesisPaperToken.getTokenValidStatusByOwner(addr1.address)).toString();
            const validStatusList = rawValidStatus.split(',');
            expect(validStatusList[0]).to.be.equal('true');
            expect(validStatusList[1]).to.be.equal('true');
            expect(validStatusList[2]).to.be.equal('true');
            expect(validStatusList[3]).to.be.equal('false');

            await expectRevert(
                genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 2),
                "InvalidToken()"
            );
            await genesisPaperToken.connect(owner).setTokenValid(2);
            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 2);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("7");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("3");

            await expectRevert(
                genesisPaperToken.connect(addr1).setTokenValidInBatch([3,4]),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setTokenValidInBatch([3,4]);
            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 3);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("6");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("4");

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 4);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("5");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("5");
        });

        it("Unauthorized - Set Print Status", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await expectRevert(
                genesisPaperToken.connect(addr1).setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true),
                "NotAuthorized()"
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;

            genesisPaperToken.connect(addr1).setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Whitelist Mint", async function() {
            const buyer = addr1.address;
            const maxQuant = 10;
            const messageHash = ethers.utils.solidityKeccak256([ "address", "uint256" ], [ buyer, maxQuant ]);
            const signature = await owner.signMessage(ethers.utils.arrayify(messageHash));

            await expectRevert(
                genesisPaperToken.connect(addr1).setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true),
                "NotAuthorized()"
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;

            await genesisPaperToken.connect(addr1).setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature, {value: ethers.utils.parseEther('0.04')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("4");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('3');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Public Mint", async function() {
            await expectRevert(
                genesisPaperToken.connect(addr1).setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true),
                "NotAuthorized()"
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;

            await genesisPaperToken.connect(addr1).setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(10, {value: ethers.utils.parseEther('0.1')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Dutch Auction Mint", async function() {
            await expectRevert(
                genesisPaperToken.connect(addr1).setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true),
                "NotAuthorized()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 10),
                'Ownable: caller is not the owner'
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;

            await genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 9);
            await genesisPaperToken.connect(addr1).setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true);
            const price1 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price1)).to.be.equal('0.1');
            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            const price2 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price2)).to.be.equal('0.09');
            await network.provider.send("evm_increaseTime", [240]);
            await network.provider.send("evm_mine");
            const price3 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price3)).to.be.equal('0.07');

            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(10, {value: ethers.utils.parseEther('0.7')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Invalid Input - Set Dutch Auction Mint", async function() {
            await expectRevert(
                genesisPaperToken.connect(addr1).setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true),
                "NotAuthorized()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 10),
                'Ownable: caller is not the owner'
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;         

            await expectRevert(
                genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.2'), ethers.utils.parseEther('0.01'), 10),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 10),
                "InvalidInput()"
            );

            await genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 9);
            await genesisPaperToken.connect(addr1).setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true);

            await expectRevert(
                genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 9),
                "DuringSales()"
            );

            const price1 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price1)).to.be.equal('0.1');
            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            const price2 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price2)).to.be.equal('0.09');
            await network.provider.send("evm_increaseTime", [240]);
            await network.provider.send("evm_mine");
            const price3 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price3)).to.be.equal('0.07');

            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(10, {value: ethers.utils.parseEther('0.7')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Invalid Timestamp - Set Each Mint", async function() {
            await expectRevert(
                genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())-360).toString(), true),
                "InvalidInput()"
            );
            await expectRevert(
                genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())-360).toString(), true),
                "InvalidInput()"
            );
            await expectRevert(
                genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())-360).toString(), true),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true),
                "NotAuthorized()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 10),
                'Ownable: caller is not the owner'
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;         

            await expectRevert(
                genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.2'), ethers.utils.parseEther('0.01'), 10),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 10),
                "InvalidInput()"
            );

            await genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 9);
            await genesisPaperToken.connect(addr1).setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true);

            await expectRevert(
                genesisPaperToken.connect(owner).setDutchAuctionSaleInfo(120, ethers.utils.parseEther('0.1'), ethers.utils.parseEther('0.01'), ethers.utils.parseEther('0.01'), 9),
                "DuringSales()"
            );

            const price1 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price1)).to.be.equal('0.1');
            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            const price2 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price2)).to.be.equal('0.09');
            await network.provider.send("evm_increaseTime", [240]);
            await network.provider.send("evm_mine");
            const price3 = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price3)).to.be.equal('0.07');

            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(10, {value: ethers.utils.parseEther('0.7')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Authorized Status", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await expectRevert(
                genesisPaperToken.connect(addr1).setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true),
                "NotAuthorized()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr2).setAuthorized(addr1.address, true),
                'Ownable: caller is not the owner'
            );

            await expectRevert(
                genesisPaperToken.connect(addr2).setAuthorizedInBatch([addr1.address, addr2.address], [true, true]),
                'Ownable: caller is not the owner'
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            await genesisPaperToken.connect(owner).setAuthorizedInBatch([addr1.address, addr2.address], [true, true]);
            
            let authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;
            authorizedStatus = await genesisPaperToken.isAuthorized(addr2.address);
            expect(authorizedStatus).to.be.true;

            genesisPaperToken.connect(addr1).setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            genesisPaperToken.connect(addr2).setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Signer", async function() {
            const buyer = addr1.address;
            const maxQuant = 10;
            const messageHash = ethers.utils.solidityKeccak256([ "address", "uint256" ], [ buyer, maxQuant ]);
            const signature11 = await addr1.signMessage(ethers.utils.arrayify(messageHash));
            const signature12 = await addr2.signMessage(ethers.utils.arrayify(messageHash));
            const signature13 = await addr3.signMessage(ethers.utils.arrayify(messageHash));
            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature11, {value: ethers.utils.parseEther('0.04')}),
                "InvalidSignature()"
            );
            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature12, {value: ethers.utils.parseEther('0.04')}),
                "InvalidSignature()"
            );
            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature13, {value: ethers.utils.parseEther('0.04')}),
                "InvalidSignature()"
            );

            const signature2 = await owner.signMessage(ethers.utils.arrayify(messageHash));
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature2, {value: ethers.utils.parseEther('0.04')});
            
            await expectRevert(
                genesisPaperToken.connect(addr1).setSigner(addr1.address, true),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setSigner(addr1.address, true);
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature11, {value: ethers.utils.parseEther('0.04')});

            await expectRevert(
                genesisPaperToken.connect(addr1).setSignerInBatch([addr2.address, addr3.address], [true, true]),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setSignerInBatch([addr2.address, addr3.address], [true, true]);
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(1, maxQuant, signature12, {value: ethers.utils.parseEther('0.01')});
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(1, maxQuant, signature13, {value: ethers.utils.parseEther('0.01')});

            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Sales Amount", async function() {
            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString(), true);
            let price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');
            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.4')});

            await expectRevert(
                genesisPaperToken.connect(addr1).setMaxGenesisPapersTierAmount(10),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(10);

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.09');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(7, {value: ethers.utils.parseEther('0.63')}),
                "ExceedMaxTierGenesisPapers()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setMaxGenesisPapersAmount(9),
                'Ownable: caller is not the owner'
            );

            await genesisPaperToken.connect(owner).setMaxGenesisPapersAmount(9);

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.08');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(6, {value: ethers.utils.parseEther('0.48')}),
                "ExceedMaxGenesisPapers()"
            );

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.07');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(0, {value: ethers.utils.parseEther('0')}),
                "ZeroQuantity()"
            );

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.06');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(1, {value: ethers.utils.parseEther('0')}),
                "NotEnoughETH()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setMaxGenesisPapersPerTx(1),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setMaxGenesisPapersPerTx(1);

            await network.provider.send("evm_increaseTime", [240]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.04');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(2, {value: ethers.utils.parseEther('0.08')}),
                "MintTooManyInOneTx()"
            );

            await network.provider.send("evm_increaseTime", [2400]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.01');

            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(1, {value: ethers.utils.parseEther('0.01')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("5");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('4');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Royalties & Price", async function() {
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(2, {value: ethers.utils.parseEther('0.02')});
            await expectRevert(
                genesisPaperToken.connect(addr1).setGenesisPapersPrice(ethers.utils.parseEther('0.02')),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.connect(owner).setGenesisPapersPrice(ethers.utils.parseEther('0.02'));
            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(2, {value: ethers.utils.parseEther('0.04')});
            await genesisPaperToken.connect(owner).setGenesisPapersPrice(ethers.utils.parseEther('0.01'));

            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(7);

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.04')}),
                "ExceedMaxTierGenesisPapers()"
            );

            await genesisPaperToken.connect(owner).setMaxGenesisPapersAmount(6);

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(3, {value: ethers.utils.parseEther('0.04')}),
                "ExceedMaxGenesisPapers()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(0, {value: ethers.utils.parseEther('0.04')}),
                "ZeroQuantity()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(1, {value: ethers.utils.parseEther('0')}),
                "NotEnoughETH()"
            );

            await genesisPaperToken.connect(owner).setMaxGenesisPapersPerTx(1);
            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(2, {value: ethers.utils.parseEther('0.02')}),
                "MintTooManyInOneTx()"
            );

            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(1, {value: ethers.utils.parseEther('0.01')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("5");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('4');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');

            await expectRevert(
                genesisPaperToken.connect(addr1).setGenesisPapersPrice(ethers.utils.parseEther('0.02')),
                'Ownable: caller is not the owner'
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setDefaultRoyalty(owner.address, 650),
                'Ownable: caller is not the owner'
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).setTokenRoyalty(2, owner.address, 650),
                'Ownable: caller is not the owner'
            );

            let royalty = await genesisPaperToken.royaltyInfo(1, ethers.utils.parseEther('1'));
            expect(royalty[1].toString()).to.be.equal(ethers.utils.parseEther('0.1').toString());

            await genesisPaperToken.connect(owner).setTokenRoyalty(2, owner.address, 650);

            royalty = await genesisPaperToken.royaltyInfo(2, ethers.utils.parseEther('1'));
            expect(royalty[1].toString()).to.be.equal(ethers.utils.parseEther('0.065').toString());

            await genesisPaperToken.connect(owner).setDefaultRoyalty(owner.address, 9999);
            royalty = await genesisPaperToken.royaltyInfo(1, ethers.utils.parseEther('1'));
            expect(royalty[1].toString()).to.be.equal(ethers.utils.parseEther('0.9999').toString());
        });

        it("Unauthorized - Set TokenURI", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            let tokenURI = await genesisPaperToken.tokenURI(0);
            expect(tokenURI).to.be.equal("https://api.timelessmucha.xyz/metadata/0")

            await expectRevert(
                genesisPaperToken.connect(addr1).setBaseURI("https://api.timelessmucha.xyz/metadata/test/"),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.setBaseURI("https://api.timelessmucha.xyz/metadata/test/");

            tokenURI = await genesisPaperToken.tokenURI(0);
            expect(tokenURI).to.be.equal("https://api.timelessmucha.xyz/metadata/test/0")

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Unauthorized - Set Treasury", async function() {
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(10, {value: ethers.utils.parseEther('0.1')});

            await expectRevert(
                genesisPaperToken.connect(addr1).setTreasury(addr1.address),
                'Ownable: caller is not the owner'
            );
            await genesisPaperToken.setTreasury(addr1.address);

            let initBalance = await ethers.provider.getBalance(addr1.address);
            await genesisPaperToken.withdraw(ethers.utils.parseEther('0.03'));
            let afterBalance = await ethers.provider.getBalance(addr1.address);
            expect((afterBalance.sub(initBalance)).toString()).to.be.equal(ethers.utils.parseEther('0.03').toString());

            initBalance = await ethers.provider.getBalance(addr1.address);
            await genesisPaperToken.withdraw(ethers.utils.parseEther('0.05'));
            afterBalance = await ethers.provider.getBalance(addr1.address);
            expect((afterBalance.sub(initBalance)).toString()).to.be.equal(ethers.utils.parseEther('0.05').toString());

            contractBalance = await ethers.provider.getBalance(genesisPaperToken.address);
            expect(contractBalance.toString()).to.be.equal(ethers.utils.parseEther('0.02').toString());

            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.PaperItem(0);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(0);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('true');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('false');
        });

        it("Happy Path - Get holders info", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");

            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 0);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("9");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("1");

            await genesisPaperToken.connect(owner).setTokenInvalid(1);
            expect((await genesisPaperToken.getTokenValidStatus(1))).to.be.true;
            
            await expectRevert(
                genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 1),
                "InvalidToken()"
            );
            await genesisPaperToken.connect(owner).setTokenValid(1);
            expect((await genesisPaperToken.getTokenValidStatus(1))).to.be.false;

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 1);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("8");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("2");

            await genesisPaperToken.connect(owner).setTokenInvalidInBatch([2,3,4]);
            expect((await genesisPaperToken.getTokenValidStatus(2))).to.be.true;
            expect((await genesisPaperToken.getTokenValidStatus(3))).to.be.true;
            expect((await genesisPaperToken.getTokenValidStatus(4))).to.be.true;

            const rawValidStatus = (await genesisPaperToken.getTokenValidStatusByOwner(addr1.address)).toString();
            const validStatusList = rawValidStatus.split(',');
            expect(validStatusList[0]).to.be.equal('true');
            expect(validStatusList[1]).to.be.equal('true');
            expect(validStatusList[2]).to.be.equal('true');
            expect(validStatusList[3]).to.be.equal('false');

            await expectRevert(
                genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 2),
                "InvalidToken()"
            );
            await genesisPaperToken.connect(owner).setTokenValid(2);
            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 2);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("7");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("3");

            await genesisPaperToken.connect(owner).setTokenValidInBatch([3,4]);
            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 3);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("6");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("4");

            await genesisPaperToken.connect(addr1).transferFrom(addr1.address, addr3.address, 4);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("5");
            expect((await genesisPaperToken.balanceOf(addr3.address)).toString()).to.be.equal("5");

            const rawHoldersList = await genesisPaperToken.getAllHolders();
            expect(rawHoldersList[0]).to.be.equal(addr3.address);
            expect(rawHoldersList[1]).to.be.equal(addr1.address);

            const rawHoldersInfoList = await genesisPaperToken.getAllHoldersInfo();
        });

        it("Unauthorized - Set Print Info", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            await genesisPaperToken.mintGiveawayGenesisPapers(addr2.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            expect((await genesisPaperToken.balanceOf(addr2.address)).toString()).to.be.equal("10");

            const rawTokensOfOwner1 = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner1 = rawTokensOfOwner1.split(",");
            expect(tokensOfOwner1[0]).to.be.equal('0');
            expect(tokensOfOwner1[tokensOfOwner1.length-1]).to.be.equal('9');

            const rawTokensOfOwner2 = (await genesisPaperToken.tokensOfOwner(addr2.address)).toString();
            const tokensOfOwner2 = rawTokensOfOwner2.split(",");
            expect(tokensOfOwner2[0]).to.be.equal('10');
            expect(tokensOfOwner2[tokensOfOwner2.length-1]).to.be.equal('19');


            await expectRevert(
                genesisPaperToken.setPrintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())-360).toString(), true),
                "InvalidInput()"
            );
            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+720).toString(), true);
            

            await expectRevert(
                genesisPaperToken.connect(addr1).printGenesisPapers(10, 1),
                "NotTokenOwner()"
            );

            await genesisPaperToken.setArtworkEditionMaxLimit(1, 1);

            await genesisPaperToken.connect(addr1).printGenesisPapers(9, 1);

            await expectRevert(
                genesisPaperToken.connect(addr2).printGenesisPapers(10, 1),
                "ExceedMaxPrintableAount()"
            );

            await genesisPaperToken.setArtworkEditionMaxLimitInBatch([...Array(40 - 1 + 1).keys()].map(x => x + 1), Array(40).fill(2));

            await expectRevert(
                genesisPaperToken.connect(addr1).printGenesisPapers(9, 1),
                "AlreadyPrinted()"
            );

            await genesisPaperToken.connect(addr2).printGenesisPapers(10, 1);

            const printRes = await genesisPaperToken.PaperItem(9);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(9);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('false');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('true');
        });

        it("Edge Case", async function() {
            await expectRevert(
                genesisPaperToken.setTreasury(constants.ZERO_ADDRESS),
                "ZeroAddress()"
            );

            await expectRevert(
                genesisPaperToken.setArtworkEditionMaxLimitInBatch([], [1]),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setSignerInBatch([], [true]),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setAuthorizedInBatch([], [true]),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.tokenURI(0),
                "TokenNotExist()"
            );

            const rawHoldersInfoList = await genesisPaperToken.getAllHoldersInfo();
            const rawHoldersList = await genesisPaperToken.getAllHolders();
            const rawTokenValidList = await genesisPaperToken.getTokenValidStatusByOwner(addr1.address);
            const rawTokenPrintStatusList = await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address);
            const tokenList = await genesisPaperToken.tokensOfOwner(addr1.address);
            expect(rawHoldersInfoList).to.be.empty;
            expect(rawHoldersList).to.be.empty;
            expect(rawTokenValidList).to.be.empty;
            expect(rawTokenPrintStatusList).to.be.empty;
            expect(tokenList).to.be.empty;
        });
    });
})