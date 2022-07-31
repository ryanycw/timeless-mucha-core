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
    });

    describe("Mint Public Mint", function() {
        it("Mint with wrong amount of ETH", async function() {
            await genesisPaperToken.setPublicMintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString());
            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.05')}),
                "InvalidAmountETH()"
            );

            await expectRevert(
                genesisPaperToken.connect(addr1).mintPublicGenesisPapers(4, {value: ethers.utils.parseEther('0.03')}),
                "InvalidAmountETH()"
            );

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
    });
})