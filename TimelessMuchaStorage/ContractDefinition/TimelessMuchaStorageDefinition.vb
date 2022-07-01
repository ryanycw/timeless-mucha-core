Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Numerics
Imports Nethereum.Hex.HexTypes
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Web3
Imports Nethereum.RPC.Eth.DTOs
Imports Nethereum.Contracts.CQS
Imports Nethereum.Contracts
Imports System.Threading
Namespace TimelessMuchaCore.Contracts.TimelessMuchaStorage.ContractDefinition

    
    
    Public Partial Class TimelessMuchaStorageDeployment
     Inherits TimelessMuchaStorageDeploymentBase
    
        Public Sub New()
            MyBase.New(DEFAULT_BYTECODE)
        End Sub
        
        Public Sub New(ByVal byteCode As String)
            MyBase.New(byteCode)
        End Sub
    
    End Class

    Public Class TimelessMuchaStorageDeploymentBase 
            Inherits ContractDeploymentMessage
        
        Public Shared DEFAULT_BYTECODE As String = "608060405234801561001057600080fd5b5061059a806100206000396000f3fe608060405234801561001057600080fd5b50600436106101cf5760003560e01c80639e67534011610104578063cb9fe586116100a2578063f32710ff11610071578063f32710ff146103c4578063fe9fbb80146103cd578063ff155c1d146103f0578063ffb14ca6146103f957600080fd5b8063cb9fe58614610387578063d60b5d7e14610394578063e3b5c61c1461039d578063f0b074c2146103b157600080fd5b8063ab72e122116100de578063ab72e12214610363578063af745c151461036c578063be57bf1814610375578063c798f3771461037e57600080fd5b80639e67534014610348578063a8a4eddc14610351578063a98bfd031461035a57600080fd5b806353190d3b1161017157806379ca897c1161014b57806379ca897c146102b15780637c1fb34e146102ff578063930b96541461030857806398a8cffe1461032857600080fd5b806353190d3b1461028057806361d027b3146102895780636c0360eb1461029c57600080fd5b8063238ac933116101ad578063238ac93314610219578063262023701461024c5780632b3e4c421461026e578063333ee00e1461027757600080fd5b806302ddfd21146101d4578063105c9f6d146101f05780631ff36a96146101f9575b600080fd5b6101dd600f5481565b6040519081526020015b60405180910390f35b6101dd60105481565b6101dd6102073660046104be565b60026020526000908152604090205481565b6017546102349064010000000090046001600160a01b031681565b6040516001600160a01b0390911681526020016101e7565b60175461025e90610100900460ff1681565b60405190151581526020016101e7565b6101dd600c5481565b6101dd60155481565b6101dd60075481565b601854610234906001600160a01b031681565b6102a4610402565b6040516101e791906104d6565b6102e26102bf3660046104be565b600160208190526000918252604090912080549181015460029091015460ff1683565b6040805193845260208401929092521515908201526060016101e7565b6101dd600e5481565b6101dd6103163660046104be565b60036020526000908152604090205481565b6101dd610336366004610490565b60006020819052908152604090205481565b6101dd60125481565b6101dd60095481565b6101dd60065481565b6101dd600d5481565b6101dd600b5481565b6101dd600a5481565b6101dd60145481565b60175461025e9060ff1681565b6101dd60165481565b60175461025e906301000000900460ff1681565b60175461025e9062010000900460ff1681565b6101dd60115481565b61025e6103db366004610490565b60046020526000908152604090205460ff1681565b6101dd60135481565b6101dd60085481565b6019805461040f90610529565b80601f016020809104026020016040519081016040528092919081815260200182805461043b90610529565b80156104885780601f1061045d57610100808354040283529160200191610488565b820191906000526020600020905b81548152906001019060200180831161046b57829003601f168201915b505050505081565b6000602082840312156104a1578081fd5b81356001600160a01b03811681146104b7578182fd5b9392505050565b6000602082840312156104cf578081fd5b5035919050565b6000602080835283518082850152825b81811015610502578581018301518582016040015282016104e6565b818111156105135783604083870101525b50601f01601f1916929092016040019392505050565b600181811c9082168061053d57607f821691505b6020821081141561055e57634e487b7160e01b600052602260045260246000fd5b5091905056fea2646970667358221220e9ead31c8f4a355044489285b2579b3dabbcb7d7f192d9992374a0068b8b9c4564736f6c63430008040033"
        
        Public Sub New()
            MyBase.New(DEFAULT_BYTECODE)
        End Sub
        
        Public Sub New(ByVal byteCode As String)
            MyBase.New(byteCode)
        End Sub
        

    
    End Class    
    
    Public Partial Class MaxMuchapapersAmountFunction
        Inherits MaxMuchapapersAmountFunctionBase
    End Class

        <[Function]("MAX_MUCHAPAPERS_AMOUNT", "uint256")>
    Public Class MaxMuchapapersAmountFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MaxMuchapapersPertxFunction
        Inherits MaxMuchapapersPertxFunctionBase
    End Class

        <[Function]("MAX_MUCHAPAPERS_PERTX", "uint256")>
    Public Class MaxMuchapapersPertxFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MaxMuchapapersTieramountFunction
        Inherits MaxMuchapapersTieramountFunctionBase
    End Class

        <[Function]("MAX_MUCHAPAPERS_TIERAMOUNT", "uint256")>
    Public Class MaxMuchapapersTieramountFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MuchapapersPriceFunction
        Inherits MuchapapersPriceFunctionBase
    End Class

        <[Function]("MUCHAPAPERS_PRICE", "uint256")>
    Public Class MuchapapersPriceFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PaperItemFunction
        Inherits PaperItemFunctionBase
    End Class

        <[Function]("PaperItem", GetType(PaperItemOutputDTO))>
    Public Class PaperItemFunctionBase
        Inherits FunctionMessage
    
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class
    
    
    Public Partial Class BaseURIFunction
        Inherits BaseURIFunctionBase
    End Class

        <[Function]("baseURI", "string")>
    Public Class BaseURIFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class DutchAuctionMintEndTimestampFunction
        Inherits DutchAuctionMintEndTimestampFunctionBase
    End Class

        <[Function]("dutchAuctionMintEndTimestamp", "uint256")>
    Public Class DutchAuctionMintEndTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class DutchAuctionMintStartTimestampFunction
        Inherits DutchAuctionMintStartTimestampFunctionBase
    End Class

        <[Function]("dutchAuctionMintStartTimestamp", "uint256")>
    Public Class DutchAuctionMintStartTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class DutchAuctionMintStatusFunction
        Inherits DutchAuctionMintStatusFunctionBase
    End Class

        <[Function]("dutchAuctionMintStatus", "bool")>
    Public Class DutchAuctionMintStatusFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class FormLimitAmountFunction
        Inherits FormLimitAmountFunctionBase
    End Class

        <[Function]("formLimitAmount", "uint256")>
    Public Class FormLimitAmountFunctionBase
        Inherits FunctionMessage
    
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class
    
    
    Public Partial Class FormPrintedAmountFunction
        Inherits FormPrintedAmountFunctionBase
    End Class

        <[Function]("formPrintedAmount", "uint256")>
    Public Class FormPrintedAmountFunctionBase
        Inherits FunctionMessage
    
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class
    
    
    Public Partial Class IsAuthorizedFunction
        Inherits IsAuthorizedFunctionBase
    End Class

        <[Function]("isAuthorized", "bool")>
    Public Class IsAuthorizedFunctionBase
        Inherits FunctionMessage
    
        <[Parameter]("address", "", 1)>
        Public Overridable Property [ReturnValue1] As String
    
    End Class
    
    
    Public Partial Class MuchaPapersAuctionSaleEndPriceFunction
        Inherits MuchaPapersAuctionSaleEndPriceFunctionBase
    End Class

        <[Function]("muchaPapersAuctionSaleEndPrice", "uint256")>
    Public Class MuchaPapersAuctionSaleEndPriceFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MuchaPapersAuctionSalePriceStepFunction
        Inherits MuchaPapersAuctionSalePriceStepFunctionBase
    End Class

        <[Function]("muchaPapersAuctionSalePriceStep", "uint256")>
    Public Class MuchaPapersAuctionSalePriceStepFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MuchaPapersAuctionSaleStartPriceFunction
        Inherits MuchaPapersAuctionSaleStartPriceFunctionBase
    End Class

        <[Function]("muchaPapersAuctionSaleStartPrice", "uint256")>
    Public Class MuchaPapersAuctionSaleStartPriceFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MuchaPapersAuctionSaleStepAmountFunction
        Inherits MuchaPapersAuctionSaleStepAmountFunctionBase
    End Class

        <[Function]("muchaPapersAuctionSaleStepAmount", "uint256")>
    Public Class MuchaPapersAuctionSaleStepAmountFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class MuchaPapersAuctionSaleTimeStepFunction
        Inherits MuchaPapersAuctionSaleTimeStepFunctionBase
    End Class

        <[Function]("muchaPapersAuctionSaleTimeStep", "uint256")>
    Public Class MuchaPapersAuctionSaleTimeStepFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PrintEndTimestampFunction
        Inherits PrintEndTimestampFunctionBase
    End Class

        <[Function]("printEndTimestamp", "uint256")>
    Public Class PrintEndTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PrintPhaseStatusFunction
        Inherits PrintPhaseStatusFunctionBase
    End Class

        <[Function]("printPhaseStatus", "bool")>
    Public Class PrintPhaseStatusFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PrintStartTimestampFunction
        Inherits PrintStartTimestampFunctionBase
    End Class

        <[Function]("printStartTimestamp", "uint256")>
    Public Class PrintStartTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PublicMintEndTimestampFunction
        Inherits PublicMintEndTimestampFunctionBase
    End Class

        <[Function]("publicMintEndTimestamp", "uint256")>
    Public Class PublicMintEndTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PublicMintStartTimestampFunction
        Inherits PublicMintStartTimestampFunctionBase
    End Class

        <[Function]("publicMintStartTimestamp", "uint256")>
    Public Class PublicMintStartTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class PublicMintStatusFunction
        Inherits PublicMintStatusFunctionBase
    End Class

        <[Function]("publicMintStatus", "bool")>
    Public Class PublicMintStatusFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class SignerFunction
        Inherits SignerFunctionBase
    End Class

        <[Function]("signer", "address")>
    Public Class SignerFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class TreasuryFunction
        Inherits TreasuryFunctionBase
    End Class

        <[Function]("treasury", "address")>
    Public Class TreasuryFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class WhitelistMintEndTimestampFunction
        Inherits WhitelistMintEndTimestampFunctionBase
    End Class

        <[Function]("whitelistMintEndTimestamp", "uint256")>
    Public Class WhitelistMintEndTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class WhitelistMintStartTimestampFunction
        Inherits WhitelistMintStartTimestampFunctionBase
    End Class

        <[Function]("whitelistMintStartTimestamp", "uint256")>
    Public Class WhitelistMintStartTimestampFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class WhitelistMintStatusFunction
        Inherits WhitelistMintStatusFunctionBase
    End Class

        <[Function]("whitelistMintStatus", "bool")>
    Public Class WhitelistMintStatusFunctionBase
        Inherits FunctionMessage
    

    
    End Class
    
    
    Public Partial Class WhitelistMintedFunction
        Inherits WhitelistMintedFunctionBase
    End Class

        <[Function]("whitelistMinted", "uint256")>
    Public Class WhitelistMintedFunctionBase
        Inherits FunctionMessage
    
        <[Parameter]("address", "", 1)>
        Public Overridable Property [ReturnValue1] As String
    
    End Class
    
    
    Public Partial Class MaxMuchapapersAmountOutputDTO
        Inherits MaxMuchapapersAmountOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MaxMuchapapersAmountOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MaxMuchapapersPertxOutputDTO
        Inherits MaxMuchapapersPertxOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MaxMuchapapersPertxOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MaxMuchapapersTieramountOutputDTO
        Inherits MaxMuchapapersTieramountOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MaxMuchapapersTieramountOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MuchapapersPriceOutputDTO
        Inherits MuchapapersPriceOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MuchapapersPriceOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class PaperItemOutputDTO
        Inherits PaperItemOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PaperItemOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "formId", 1)>
        Public Overridable Property [FormId] As BigInteger
        <[Parameter]("uint256", "formSerial", 2)>
        Public Overridable Property [FormSerial] As BigInteger
        <[Parameter]("bool", "printed", 3)>
        Public Overridable Property [Printed] As Boolean
    
    End Class    
    
    Public Partial Class BaseURIOutputDTO
        Inherits BaseURIOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class BaseURIOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("string", "", 1)>
        Public Overridable Property [ReturnValue1] As String
    
    End Class    
    
    Public Partial Class DutchAuctionMintEndTimestampOutputDTO
        Inherits DutchAuctionMintEndTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class DutchAuctionMintEndTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class DutchAuctionMintStartTimestampOutputDTO
        Inherits DutchAuctionMintStartTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class DutchAuctionMintStartTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class DutchAuctionMintStatusOutputDTO
        Inherits DutchAuctionMintStatusOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class DutchAuctionMintStatusOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("bool", "", 1)>
        Public Overridable Property [ReturnValue1] As Boolean
    
    End Class    
    
    Public Partial Class FormLimitAmountOutputDTO
        Inherits FormLimitAmountOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class FormLimitAmountOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class FormPrintedAmountOutputDTO
        Inherits FormPrintedAmountOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class FormPrintedAmountOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class IsAuthorizedOutputDTO
        Inherits IsAuthorizedOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class IsAuthorizedOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("bool", "", 1)>
        Public Overridable Property [ReturnValue1] As Boolean
    
    End Class    
    
    Public Partial Class MuchaPapersAuctionSaleEndPriceOutputDTO
        Inherits MuchaPapersAuctionSaleEndPriceOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MuchaPapersAuctionSaleEndPriceOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MuchaPapersAuctionSalePriceStepOutputDTO
        Inherits MuchaPapersAuctionSalePriceStepOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MuchaPapersAuctionSalePriceStepOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MuchaPapersAuctionSaleStartPriceOutputDTO
        Inherits MuchaPapersAuctionSaleStartPriceOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MuchaPapersAuctionSaleStartPriceOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MuchaPapersAuctionSaleStepAmountOutputDTO
        Inherits MuchaPapersAuctionSaleStepAmountOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MuchaPapersAuctionSaleStepAmountOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class MuchaPapersAuctionSaleTimeStepOutputDTO
        Inherits MuchaPapersAuctionSaleTimeStepOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class MuchaPapersAuctionSaleTimeStepOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class PrintEndTimestampOutputDTO
        Inherits PrintEndTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PrintEndTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class PrintPhaseStatusOutputDTO
        Inherits PrintPhaseStatusOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PrintPhaseStatusOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("bool", "", 1)>
        Public Overridable Property [ReturnValue1] As Boolean
    
    End Class    
    
    Public Partial Class PrintStartTimestampOutputDTO
        Inherits PrintStartTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PrintStartTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class PublicMintEndTimestampOutputDTO
        Inherits PublicMintEndTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PublicMintEndTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class PublicMintStartTimestampOutputDTO
        Inherits PublicMintStartTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PublicMintStartTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class PublicMintStatusOutputDTO
        Inherits PublicMintStatusOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class PublicMintStatusOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("bool", "", 1)>
        Public Overridable Property [ReturnValue1] As Boolean
    
    End Class    
    
    Public Partial Class SignerOutputDTO
        Inherits SignerOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class SignerOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("address", "", 1)>
        Public Overridable Property [ReturnValue1] As String
    
    End Class    
    
    Public Partial Class TreasuryOutputDTO
        Inherits TreasuryOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class TreasuryOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("address", "", 1)>
        Public Overridable Property [ReturnValue1] As String
    
    End Class    
    
    Public Partial Class WhitelistMintEndTimestampOutputDTO
        Inherits WhitelistMintEndTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class WhitelistMintEndTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class WhitelistMintStartTimestampOutputDTO
        Inherits WhitelistMintStartTimestampOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class WhitelistMintStartTimestampOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class    
    
    Public Partial Class WhitelistMintStatusOutputDTO
        Inherits WhitelistMintStatusOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class WhitelistMintStatusOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("bool", "", 1)>
        Public Overridable Property [ReturnValue1] As Boolean
    
    End Class    
    
    Public Partial Class WhitelistMintedOutputDTO
        Inherits WhitelistMintedOutputDTOBase
    End Class

    <[FunctionOutput]>
    Public Class WhitelistMintedOutputDTOBase
        Implements IFunctionOutputDTO
        
        <[Parameter]("uint256", "", 1)>
        Public Overridable Property [ReturnValue1] As BigInteger
    
    End Class
End Namespace
