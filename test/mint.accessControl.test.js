require("dotenv/config");
const fs = require('fs');
const utils = require("../helpers/utils");
const { BN, constants, expectEvent, expectRevert } = require('@openzeppelin/test-helpers');
const { expect } = require("hardhat");

describe("Timeless  Mucha", function() {
    let oneGwei, gasPrice, chainId;
    beforeEach(async function () {
        [owner, addr1, addr2,...addrs] = await ethers.getSigners();
        chainId = await ethers.provider.getNetwork();
        gasPrice = ethers.BigNumber.from("90")
        oneGwei = ethers.BigNumber.from("1000000000");
        const genesisPaperContract = await ethers.getContractFactory("TimelessMucha", owner);
        genesisPaperToken = await genesisPaperContract.deploy("", ethers.utils.parseEther("0.01"));
        await genesisPaperToken.setArtworkEditionMaxLimitInBatch([...Array(40 - 1 + 1).keys()].map(x => x + 1), Array(40).fill(40));
    });

    describe("Test Mint Related Functions", function() {
        it("Unauthorized - Giveaway Mint and Print", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");

            await expectRevert(
                genesisPaperToken.connect(addr1).mintGiveawayGenesisPapers(addr1.address, 10),
                "()"
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;

            await genesisPaperToken.connect(addr1).mintGiveawayGenesisPapers(addr1.address, 10);

            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal('20');
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('19');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(19, 1);
            const printRes = await genesisPaperToken.paperItem(19);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(19);
            expect(printStatus).to.be.true;

            const rawPrintStatus = (await genesisPaperToken.getTokenPrintStatusByOwner(addr1.address)).toString();
            const printStatusList = rawPrintStatus.split(',');
            expect(printStatusList[0]).to.be.equal('false');
            expect(printStatusList[printStatusList.length-1]).to.be.equal('true');
        });

        it("Exceed Limit Amount - Giveaway Mint and Print", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 1860);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("1860");

            await expectRevert(
                genesisPaperToken.connect(addr1).mintGiveawayGenesisPapers(addr1.address, 10),
                "NotAuthorized()"
            );

            await genesisPaperToken.connect(owner).setAuthorized(addr1.address, true);
            
            const authorizedStatus = await genesisPaperToken.isAuthorized(addr1.address);
            expect(authorizedStatus).to.be.true;

            await expectRevert(
                genesisPaperToken.connect(addr1).mintGiveawayGenesisPapers(addr1.address, 10),
                "ExceedMaxGenesisPapers()"
            );

            await genesisPaperToken.connect(owner).setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(19, 1);
            const printRes = await genesisPaperToken.paperItem(19);
            expect(printRes.artworkId.toString()).to.be.equal('1');
            expect(printRes.editionNumber.toString()).to.be.equal('1');
            expect(printRes.printed).to.be.true;

            const printStatus = await genesisPaperToken.getTokenPrintStatus(19);
            expect(printStatus).to.be.true;
        });

        it("Invalid Timestamp - Whitelist Mint and Print", async function() {
            const buyer = addr1.address;
            const maxQuant = 10;
            const messageHash = ethers.utils.solidityKeccak256([ "address", "uint256" ], [ buyer, maxQuant ]);
            const signature = await owner.signMessage(ethers.utils.arrayify(messageHash));

            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())+720).toString());
            
            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature, {value: ethers.utils.parseEther('0.04')}),
                "NotStarted()"
            );

            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())-120).toString());
            
            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature, {value: ethers.utils.parseEther('0.04')}),
                "Ended()"
            );

            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime()+360)).toString());

            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime()+360)).toString());

            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature, {value: ethers.utils.parseEther('0.04')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("4");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('3');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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

        it("Invalid Signature - Whitelist Mint and Print", async function() {
            const buyer = addr1.address;
            const maxQuant = 10;
            const messageHash = ethers.utils.solidityKeccak256([ "address", "uint256" ], [ buyer, maxQuant ]);
            const signature1 = await addr1.signMessage(ethers.utils.arrayify(messageHash));
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature1, {value: ethers.utils.parseEther('0.04')}),
                "InvalidSignature()"
            );

            const signature2 = await owner.signMessage(ethers.utils.arrayify(messageHash));
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature2, {value: ethers.utils.parseEther('0.04')});
            
            await genesisPaperToken.connect(owner).setSigner(addr1.address, true);
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature1, {value: ethers.utils.parseEther('0.04')});

            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("8");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('7');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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

        it("Invalid Amount & Price - Whitelist Mint and Print", async function() {
            const buyer = addr1.address;
            const maxQuant = 10;
            const messageHash = ethers.utils.solidityKeccak256([ "address", "uint256" ], [ buyer, maxQuant ]);
            const signature = await owner.signMessage(ethers.utils.arrayify(messageHash));
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature, {value: ethers.utils.parseEther('0.04')});

            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(9, maxQuant, signature, {value: ethers.utils.parseEther('0.09')}),
                "NotEnoughQuota()"
            );

            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())+361).toString());
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(7);
            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+361).toString());

            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(4, maxQuant, signature, {value: ethers.utils.parseEther('0.04')}),
                "ExceedMaxTierGenesisPapers()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(1, maxQuant, signature, {value: ethers.utils.parseEther('0')}),
                "InvalidAmountETH()"
            );

            await genesisPaperToken.connect(addr1).mintWhitelistGenesisPapers(2, maxQuant, signature, {value: ethers.utils.parseEther('0.02')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("6");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('5');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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

        it("Invalid Timestamp - Public Mint and Print", async function() {
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())+720).toString());
            
            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.04')}),
                "NotStarted()"
            );

            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())-120).toString());
            
            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.04')}),
                "Ended()"
            );

            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime()+360)).toString());

            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.04')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("4");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('3');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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

        it("Invalid Amount & Price - Public Mint and Print", async function() {
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.04')});

            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())+361).toString());
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(7);
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+361).toString());

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.04')}),
                "ExceedMaxTierGenesisPapers()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(1, {value: ethers.utils.parseEther('0')}),
                "InvalidAmountETH()"
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

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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

        it("Invalid Timestamp - Dutch Auction Mint and Print", async function() {
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await network.provider.send("evm_increaseTime", [1200]);
            await network.provider.send("evm_mine");

            await genesisPaperToken.setDutchAuctionSaleInfo(120, ethers.utils.parseEther("0.1"), ethers.utils.parseEther("0.01"), ethers.utils.parseEther("0.01"), 9);

            let price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');

            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())+720).toString());
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.04')}),
                "NotStarted()"
            );

            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())-120).toString());
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.04')}),
                "Ended()"
            );

            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime()+360)).toString());
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.07');

            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString());
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');
            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.09');
            await network.provider.send("evm_increaseTime", [240]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.07');

            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(10, {value: ethers.utils.parseEther('0.7')});
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");
            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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

        it("Invalid Amount & Price - Dutch Auction Mint and Print", async function() {
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setDutchAuctionSaleInfo(120, ethers.utils.parseEther("0.1"), ethers.utils.parseEther("0.01"), ethers.utils.parseEther("0.01"), 9);
            
            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString());
            let price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');
            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.4')});

            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())+3600).toString(), ((await utils.getCurTime())+3601).toString());
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(10);
            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString());

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.09');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(7, {value: ethers.utils.parseEther('0.63')}),
                "ExceedMaxTierGenesisPapers()"
            );

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.08');

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.07');

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.06');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(1, {value: ethers.utils.parseEther('0')}),
                "InvalidAmountETH()"
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

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).printGenesisPapers(0, 1);
            const printRes = await genesisPaperToken.paperItem(0);
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
    });
})