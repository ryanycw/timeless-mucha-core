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
        it("Set max amount to zero", async function() {
            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersAmount(0),
                "InvalidInput()"
            );
        });

        it("Set max amount less than tier amount", async function() {
            await genesisPaperToken.setMaxGenesisPapersTierAmount(100);
            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersAmount(99),
                "InvalidInput()"
            );
        });

        it("Set max amount less than current total supply", async function() {
            await genesisPaperToken.mintGiveawayGenesisPapers(addr1.address, 10);
            await expectRevert(
                genesisPaperToken.setMaxGenesisPapersAmount(9),
                "InvalidInput()"
            );
        });
    });
})