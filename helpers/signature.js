async function genWordSignature(signer, addr, quantity, chainId, contractAddr) {
    var signature = await signer._signTypedData(
        // Domain
        {
            name: 'MintverseWord',
            version: '1.0.0',
            chainId: chainId,
            verifyingContract: contractAddr,
        },
        // Types
        {
            NFT: [
                {
                    name: 'addressForClaim',
                    type: 'address'
                },
                {
                    name: 'maxQuantity',
                    type: 'uint256'
                },
            ],
        },
        // Value
        { 
            addressForClaim: addr,
            maxQuantity: quantity
        },
      );
    return signature;
}

async function genMintDictionarySignature(signer, addr, quantity, chainId, contractAddr) {
    var signature = await signer._signTypedData(
        // Domain
        {
            name: 'MintverseDictionary',
            version: '1.0.0',
            chainId: chainId,
            verifyingContract: contractAddr,
        },
        // Types
        {
            MINT: [
                {
                    name: 'addressForClaim',
                    type: 'address'
                },
                {
                    name: 'maxQuantity',
                    type: 'uint256'
                },
            ],
        },
        // Value
        { 
            addressForClaim: addr,
            maxQuantity: quantity
        },
      );
    return signature;
}

async function genClaimDictionarySignature(signer, addr, quantity, chainId, contractAddr) {
    var signature = await signer._signTypedData(
        // Domain
        {
            name: 'MintverseDictionary',
            version: '1.0.0',
            chainId: chainId,
            verifyingContract: contractAddr,
        },
        // Types
        {
            CLAIM: [
                {
                    name: 'addressForClaim',
                    type: 'address'
                },
                {
                    name: 'maxQuantity',
                    type: 'uint256'
                },
            ],
        },
        // Value
        { 
            addressForClaim: addr,
            maxQuantity: quantity
        },
      );
    return signature;
}

module.exports = { genWordSignature, genMintDictionarySignature, genClaimDictionarySignature };