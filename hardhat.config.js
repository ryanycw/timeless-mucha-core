require("dotenv/config");
require('@openzeppelin/hardhat-upgrades');
require("@nomiclabs/hardhat-truffle5");
require("@nomiclabs/hardhat-etherscan");
require("hardhat-gas-reporter");
require('solidity-coverage');

accInfo = [`0x${process.env.MINTVERSE_DEPLOY_PK}`,
           `0x${process.env.RPF_DEPLOY_PK}`,
           `0x${process.env.RPF_DEV_PK}`,
           `0x${process.env.PK1}`,
           `0x${process.env.PK2}`,]

accInfoTest = [`0x${process.env.MINTVERSE_DEPLOY_PK}`,
               `0x${process.env.RPF_DEPLOY_PK}`,
               `0x${process.env.RPF_DEV_PK}`,
               `0x${process.env.PK1}`,
               `0x${process.env.PK2}`,]

accInfoLocal = [{privateKey: `0x${process.env.MINTVERSE_DEPLOY_PK}`, balance: (25.056033348118027032 * 1e18).toString()},
                {privateKey: `0x${process.env.RPF_DEPLOY_PK}`, balance: (10.056033348118027032 * 1e18).toString()},
                {privateKey: `0x${process.env.RPF_DEV_PK}`, balance: (5.056033348118027032 * 1e18).toString()},
                {privateKey: `0x${process.env.PK1}`, balance: (4.056033348118027032 * 1e18).toString()},
                {privateKey: `0x${process.env.PK2}`, balance: (1.056033348118027032 * 1e18).toString()},]

module.exports = {
  networks: {
    mainnet: {
      url: process.env.INFURA_MAINNET_URL,
      accounts: accInfo,
    },
    rinkeby: {
      url: process.env.INFURA_RINKEBY_URL,
      accounts: accInfoTest,
    },
    hardhat: {
      accounts: {
        "mnemonic": ""
      },
      forking: {
        url: process.env.INFURA_MAINNET_URL,
        //blockNumber:,
        enabled: true,
      },
    },
  },
  solidity: {
    compilers: [
      {
        version: "0.8.7",
        settings: {
          optimizer: {
            enabled: true,
            runs: 100,
          },
        },
      },
      {
        version: "0.8.4",
        settings: {
          optimizer: {
            enabled: true,
            runs: 100,
          },
        },
      },
      {
        version: "0.8.2",
        settings: {
          optimizer: {
            enabled: true,
            runs: 100,
          },
        },
      },
      {
        version: "0.7.3",
        settings: {
          optimizer: {
            enabled: true,
            runs: 100,
          },
        },
      },
      {
        version: "0.4.21",
        settings: {
          optimizer: {
            enabled: true,
            runs: 100,
          },
        },
      },
    ],
  },
  mocha: {
    timeout: 300 * 1e3,
  },
  etherscan: {
    apiKey: {
      mainnet: process.env.ETHERSCAN_API_KEY,
      rinkeby: process.env.ETHERSCAN_API_KEY,
    }
  }
};