require("dotenv/config");
const fs = require('fs');
const utils = require("../helpers/utils");
const sign = require("../helpers/signature")
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
        genesisPaperToken = await genesisPaperContract.deploy();
        await genesisPaperToken.setArtworkEditionMaxLimitInBatch([...Array(40 - 1 + 1).keys()].map(x => x + 1), Array(40).fill(40));
    });

    describe("Test Print Related Functions", function() {
        it("Invalid Timestamp - Print", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            expect((await genesisPaperToken.balanceOf(addr1.address)).toString()).to.be.equal("10");

            const rawTokensOfOwner = (await genesisPaperToken.tokensOfOwner(addr1.address)).toString();
            const tokensOfOwner = rawTokensOfOwner.split(",");
            expect(tokensOfOwner[0]).to.be.equal('0');
            expect(tokensOfOwner[tokensOfOwner.length-1]).to.be.equal('9');

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())+360).toString(), ((await utils.getCurTime())+720).toString(), true);
            
            await expectRevert(
                genesisPaperToken.connect(addr1).printGenesisPapers(9, 1),
                "NotStarted()"
            );

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())-120).toString(), true);
            
            await expectRevert(
                genesisPaperToken.connect(addr1).printGenesisPapers(9, 1),
                "Ended()"
            );

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime()+360)).toString(), false);
            
            await expectRevert(
                genesisPaperToken.connect(addr1).printGenesisPapers(9, 1),
                "Inactive()"
            );

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())-360).toString(), ((await utils.getCurTime())+360).toString(), true);
            await genesisPaperToken.connect(addr1).printGenesisPapers(9, 1);
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

        it("Unauthorized - Print", async function() {
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

            await genesisPaperToken.setPrintPhase(((await utils.getCurTime())).toString(), ((await utils.getCurTime())+720).toString(), true);
            

            await expectRevert(
                genesisPaperToken.connect(addr1).printGenesisPapers(10, 1),
                "NotTokenOwner()"
            );

            await genesisPaperToken.setArtworkEditionMaxLimitInBatch([...Array(40 - 1 + 1).keys()].map(x => x + 1), Array(40).fill(1));

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
    });
})