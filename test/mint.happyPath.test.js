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
        it("Happy Path - Giveaway Mint and Print", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
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

        it("Happy Path - Whitelist Mint and Print", async function() {
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            const buyer = addr1.address;
            const maxQuant = 10;
            const messageHash = ethers.utils.solidityKeccak256([ "address", "uint256" ], [ buyer, maxQuant ]);
            const signature = await owner.signMessage(ethers.utils.arrayify(messageHash));
            await genesisPaperToken.setWhitelistMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
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

        it("Happy Path - Public Mint and Print", async function() {
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await genesisPaperToken.connect(addr1).mintPublicGenesisPapers(10, {value: ethers.utils.parseEther('0.1')});
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

        it("Happy Path - Dutch Auction Mint and Print", async function() {
            await genesisPaperToken.setDutchAuctionSaleInfo(120, ethers.utils.parseEther("0.1"), ethers.utils.parseEther("0.01"), ethers.utils.parseEther("0.01"), 9);
            await genesisPaperToken.connect(owner).setMaxGenesisPapersTierAmount(200);
            await genesisPaperToken.setDutchAuctionMintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+3600).toString());
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