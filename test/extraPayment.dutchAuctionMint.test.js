require("dotenv/config");
const fs = require('fs');
const utils = require("../helpers/utils");
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
        genesisPaperToken = await genesisPaperContract.deploy("", ethers.utils.parseEther("0.01"));
        await genesisPaperToken.setArtworkEditionMaxLimitInBatch([...Array(40 - 1 + 1).keys()].map(x => x + 1), Array(40).fill(40));
        await genesisPaperToken.setMaxGenesisPapersTierAmount(200);
        await genesisPaperToken.setDutchAuctionSaleInfo(120, ethers.utils.parseEther("0.1"), ethers.utils.parseEther("0.01"), ethers.utils.parseEther("0.01"), 9);
    });

    describe("Mint Dutch Auction Mint", function() {
        it("Mint with wrong amount of ETH", async function() {
            let initTime = await utils.getCurTime();
            await genesisPaperToken.setDutchAuctionMintPhase(initTime.toString(), (initTime+1800).toString());

            let price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.1');

            await network.provider.send("evm_increaseTime", [480]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.06');

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.25')}),
                "InvalidAmountETH()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.2')}),
                "InvalidAmountETH()"
            );

            await network.provider.send("evm_increaseTime", [120]);
            await network.provider.send("evm_mine");
            price = await genesisPaperToken.getDutchAuctionPrice();
            expect(ethers.utils.formatEther(price)).to.be.equal('0.05');

            await genesisPaperToken.connect(addr1).mintDutchAuctionGenesisPapers(4, {value: ethers.utils.parseEther('0.2')});

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
    });
})