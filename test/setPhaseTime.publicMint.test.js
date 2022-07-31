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
        await genesisPaperToken.setDutchAuctionSaleInfo(120, ethers.utils.parseEther("0.1"), ethers.utils.parseEther("0.01"), ethers.utils.parseEther("0.01"), 9);
    });

    describe("Set Public Mint", function() {
        it("Set timestamp when tier amount is zero", async function() {
            let initTime = await utils.getCurTime();

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+10).toString(), initTime.toString()),
                "SalesInfoUnset()"
            );

            await genesisPaperToken.setMaxGenesisPapersTierAmount(10);

            await genesisPaperToken.setPublicMintPhase(initTime.toString(), (initTime+5).toString());
        });

        it("Set start time after end time", async function() {
            let initTime = await utils.getCurTime();

            await genesisPaperToken.setMaxGenesisPapersTierAmount(10);

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+10).toString(), initTime.toString()),
                "InvalidInput()"
            );

            await genesisPaperToken.setPublicMintPhase(initTime.toString(), (initTime+5).toString());
        });

        it("Set time collapsed with whitelist mint phase", async function() {
            let initTime = await utils.getCurTime();

            await genesisPaperToken.setMaxGenesisPapersTierAmount(10);

            await genesisPaperToken.setWhitelistMintPhase((initTime+10).toString(), (initTime+20).toString());
            await genesisPaperToken.setDutchAuctionMintPhase((initTime+40).toString(), (initTime+50).toString());

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+5).toString(), (initTime+10).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+5).toString(), (initTime+15).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+5).toString(), (initTime+20).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+5).toString(), (initTime+25).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+10).toString(), (initTime+15).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+10).toString(), (initTime+20).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+10).toString(), (initTime+25).toString()),
                "InvalidInput()"
            );
            
            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+15).toString(), (initTime+18).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+15).toString(), (initTime+20).toString()),
                "InvalidInput()"
            );
            
            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+15).toString(), (initTime+25).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+20).toString(), (initTime+25).toString()),
                "InvalidInput()"
            );

            await genesisPaperToken.setPublicMintPhase(initTime.toString(), (initTime+5).toString());
        });

        it("Set time collapsed with dutch auction mint phase", async function() {
            let initTime = await utils.getCurTime();

            await genesisPaperToken.setMaxGenesisPapersTierAmount(10);

            await genesisPaperToken.setWhitelistMintPhase((initTime+10).toString(), (initTime+20).toString());
            await genesisPaperToken.setDutchAuctionMintPhase((initTime+40).toString(), (initTime+50).toString());

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+35).toString(), (initTime+40).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+35).toString(), (initTime+45).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+35).toString(), (initTime+50).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+35).toString(), (initTime+55).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+40).toString(), (initTime+45).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+40).toString(), (initTime+50).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+40).toString(), (initTime+55).toString()),
                "InvalidInput()"
            );
            
            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+45).toString(), (initTime+48).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+45).toString(), (initTime+50).toString()),
                "InvalidInput()"
            );
            
            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+45).toString(), (initTime+55).toString()),
                "InvalidInput()"
            );

            await expectRevert(
                genesisPaperToken.setPublicMintPhase((initTime+50).toString(), (initTime+55).toString()),
                "InvalidInput()"
            );

            await genesisPaperToken.setPublicMintPhase(initTime.toString(), (initTime+5).toString());
        });
    });
})