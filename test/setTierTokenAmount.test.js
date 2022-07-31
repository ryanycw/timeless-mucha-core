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
    });

    describe("Set Tier Amount", function() {
        it("Set tier amount larger than max amount", async function() {
            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersTierAmount(1861),
                "InvalidInput()"
            );
        });

        it("Set tier amount less than current total supply", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersTierAmount(9),
                "InvalidInput()"
            );
        });

        it("Set tier amount during public mint", async function() {
            let initTime = await utils.getCurTime();

            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);

            await genesisPaperToken.setMaxGenesisPapersTierAmount(100);
            await genesisPaperToken.setPublicMintPhase(initTime.toString(), (initTime+5).toString());

            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersTierAmount(101),
                "DuringSales()"
            );
        });

        it("Set tier amount during whitelist mint", async function() {
            let initTime = await utils.getCurTime();

            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);

            await genesisPaperToken.setMaxGenesisPapersTierAmount(100);
            await genesisPaperToken.setWhitelistMintPhase(initTime.toString(), (initTime+5).toString());

            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersTierAmount(101),
                "DuringSales()"
            );
        });

        it("Set tier amount during dutch auction mint", async function() {
            let initTime = await utils.getCurTime();

            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);

            await genesisPaperToken.setMaxGenesisPapersTierAmount(100);
            await genesisPaperToken.setDutchAuctionSaleInfo(120, ethers.utils.parseEther("0.1"), ethers.utils.parseEther("0.01"), ethers.utils.parseEther("0.01"), 9);
            await genesisPaperToken.setDutchAuctionMintPhase(initTime.toString(), (initTime+5).toString());

            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersTierAmount(101),
                "DuringSales()"
            );
        });
    });
})