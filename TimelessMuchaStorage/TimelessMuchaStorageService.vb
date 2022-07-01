Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Numerics
Imports Nethereum.Hex.HexTypes
Imports Nethereum.ABI.FunctionEncoding.Attributes
Imports Nethereum.Web3
Imports Nethereum.RPC.Eth.DTOs
Imports Nethereum.Contracts.CQS
Imports Nethereum.Contracts.ContractHandlers
Imports Nethereum.Contracts
Imports System.Threading
Imports TimelessMuchaCore.Contracts.TimelessMuchaStorage.ContractDefinition
Namespace TimelessMuchaCore.Contracts.TimelessMuchaStorage


    Public Partial Class TimelessMuchaStorageService
    
    
        Public Shared Function DeployContractAndWaitForReceiptAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal timelessMuchaStorageDeployment As TimelessMuchaStorageDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return web3.Eth.GetContractDeploymentHandler(Of TimelessMuchaStorageDeployment)().SendRequestAndWaitForReceiptAsync(timelessMuchaStorageDeployment, cancellationTokenSource)
        
        End Function
         Public Shared Function DeployContractAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal timelessMuchaStorageDeployment As TimelessMuchaStorageDeployment) As Task(Of String)
        
            Return web3.Eth.GetContractDeploymentHandler(Of TimelessMuchaStorageDeployment)().SendRequestAsync(timelessMuchaStorageDeployment)
        
        End Function
        Public Shared Async Function DeployContractAndGetServiceAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal timelessMuchaStorageDeployment As TimelessMuchaStorageDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of TimelessMuchaStorageService)
        
            Dim receipt = Await DeployContractAndWaitForReceiptAsync(web3, timelessMuchaStorageDeployment, cancellationTokenSource)
            Return New TimelessMuchaStorageService(web3, receipt.ContractAddress)
        
        End Function
    
        Protected Property Web3 As Nethereum.Web3.Web3
        
        Public Property ContractHandler As ContractHandler
        
        Public Sub New(ByVal web3 As Nethereum.Web3.Web3, ByVal contractAddress As String)
            Web3 = web3
            ContractHandler = web3.Eth.GetContractHandler(contractAddress)
        End Sub
    
        Public Function MaxMuchapapersAmountQueryAsync(ByVal maxMuchapapersAmountFunction As MaxMuchapapersAmountFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxMuchapapersAmountFunction, BigInteger)(maxMuchapapersAmountFunction, blockParameter)
        
        End Function

        
        Public Function MaxMuchapapersAmountQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MaxMuchapapersAmountFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MaxMuchapapersPertxQueryAsync(ByVal maxMuchapapersPertxFunction As MaxMuchapapersPertxFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxMuchapapersPertxFunction, BigInteger)(maxMuchapapersPertxFunction, blockParameter)
        
        End Function

        
        Public Function MaxMuchapapersPertxQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MaxMuchapapersPertxFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MaxMuchapapersTieramountQueryAsync(ByVal maxMuchapapersTieramountFunction As MaxMuchapapersTieramountFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MaxMuchapapersTieramountFunction, BigInteger)(maxMuchapapersTieramountFunction, blockParameter)
        
        End Function

        
        Public Function MaxMuchapapersTieramountQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MaxMuchapapersTieramountFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MuchapapersPriceQueryAsync(ByVal muchapapersPriceFunction As MuchapapersPriceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MuchapapersPriceFunction, BigInteger)(muchapapersPriceFunction, blockParameter)
        
        End Function

        
        Public Function MuchapapersPriceQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MuchapapersPriceFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PaperItemQueryAsync(ByVal paperItemFunction As PaperItemFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of PaperItemOutputDTO)
        
            Return ContractHandler.QueryDeserializingToObjectAsync(Of PaperItemFunction, PaperItemOutputDTO)(paperItemFunction, blockParameter)
        
        End Function

        
        Public Function PaperItemQueryAsync(ByVal [returnValue1] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of PaperItemOutputDTO)
        
            Dim paperItemFunction = New PaperItemFunction()
                paperItemFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryDeserializingToObjectAsync(Of PaperItemFunction, PaperItemOutputDTO)(paperItemFunction, blockParameter)
        
        End Function


        Public Function BaseURIQueryAsync(ByVal baseURIFunction As BaseURIFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of BaseURIFunction, String)(baseURIFunction, blockParameter)
        
        End Function

        
        Public Function BaseURIQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of BaseURIFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function DutchAuctionMintEndTimestampQueryAsync(ByVal dutchAuctionMintEndTimestampFunction As DutchAuctionMintEndTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of DutchAuctionMintEndTimestampFunction, BigInteger)(dutchAuctionMintEndTimestampFunction, blockParameter)
        
        End Function

        
        Public Function DutchAuctionMintEndTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of DutchAuctionMintEndTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function DutchAuctionMintStartTimestampQueryAsync(ByVal dutchAuctionMintStartTimestampFunction As DutchAuctionMintStartTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of DutchAuctionMintStartTimestampFunction, BigInteger)(dutchAuctionMintStartTimestampFunction, blockParameter)
        
        End Function

        
        Public Function DutchAuctionMintStartTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of DutchAuctionMintStartTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function DutchAuctionMintStatusQueryAsync(ByVal dutchAuctionMintStatusFunction As DutchAuctionMintStatusFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of DutchAuctionMintStatusFunction, Boolean)(dutchAuctionMintStatusFunction, blockParameter)
        
        End Function

        
        Public Function DutchAuctionMintStatusQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            return ContractHandler.QueryAsync(Of DutchAuctionMintStatusFunction, Boolean)(Nothing, blockParameter)
        
        End Function



        Public Function FormLimitAmountQueryAsync(ByVal formLimitAmountFunction As FormLimitAmountFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of FormLimitAmountFunction, BigInteger)(formLimitAmountFunction, blockParameter)
        
        End Function

        
        Public Function FormLimitAmountQueryAsync(ByVal [returnValue1] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim formLimitAmountFunction = New FormLimitAmountFunction()
                formLimitAmountFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of FormLimitAmountFunction, BigInteger)(formLimitAmountFunction, blockParameter)
        
        End Function


        Public Function FormPrintedAmountQueryAsync(ByVal formPrintedAmountFunction As FormPrintedAmountFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of FormPrintedAmountFunction, BigInteger)(formPrintedAmountFunction, blockParameter)
        
        End Function

        
        Public Function FormPrintedAmountQueryAsync(ByVal [returnValue1] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim formPrintedAmountFunction = New FormPrintedAmountFunction()
                formPrintedAmountFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of FormPrintedAmountFunction, BigInteger)(formPrintedAmountFunction, blockParameter)
        
        End Function


        Public Function IsAuthorizedQueryAsync(ByVal isAuthorizedFunction As IsAuthorizedFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of IsAuthorizedFunction, Boolean)(isAuthorizedFunction, blockParameter)
        
        End Function

        
        Public Function IsAuthorizedQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Dim isAuthorizedFunction = New IsAuthorizedFunction()
                isAuthorizedFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of IsAuthorizedFunction, Boolean)(isAuthorizedFunction, blockParameter)
        
        End Function


        Public Function MuchaPapersAuctionSaleEndPriceQueryAsync(ByVal muchaPapersAuctionSaleEndPriceFunction As MuchaPapersAuctionSaleEndPriceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleEndPriceFunction, BigInteger)(muchaPapersAuctionSaleEndPriceFunction, blockParameter)
        
        End Function

        
        Public Function MuchaPapersAuctionSaleEndPriceQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleEndPriceFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MuchaPapersAuctionSalePriceStepQueryAsync(ByVal muchaPapersAuctionSalePriceStepFunction As MuchaPapersAuctionSalePriceStepFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MuchaPapersAuctionSalePriceStepFunction, BigInteger)(muchaPapersAuctionSalePriceStepFunction, blockParameter)
        
        End Function

        
        Public Function MuchaPapersAuctionSalePriceStepQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MuchaPapersAuctionSalePriceStepFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MuchaPapersAuctionSaleStartPriceQueryAsync(ByVal muchaPapersAuctionSaleStartPriceFunction As MuchaPapersAuctionSaleStartPriceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleStartPriceFunction, BigInteger)(muchaPapersAuctionSaleStartPriceFunction, blockParameter)
        
        End Function

        
        Public Function MuchaPapersAuctionSaleStartPriceQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleStartPriceFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MuchaPapersAuctionSaleStepAmountQueryAsync(ByVal muchaPapersAuctionSaleStepAmountFunction As MuchaPapersAuctionSaleStepAmountFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleStepAmountFunction, BigInteger)(muchaPapersAuctionSaleStepAmountFunction, blockParameter)
        
        End Function

        
        Public Function MuchaPapersAuctionSaleStepAmountQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleStepAmountFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function MuchaPapersAuctionSaleTimeStepQueryAsync(ByVal muchaPapersAuctionSaleTimeStepFunction As MuchaPapersAuctionSaleTimeStepFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleTimeStepFunction, BigInteger)(muchaPapersAuctionSaleTimeStepFunction, blockParameter)
        
        End Function

        
        Public Function MuchaPapersAuctionSaleTimeStepQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of MuchaPapersAuctionSaleTimeStepFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PrintEndTimestampQueryAsync(ByVal printEndTimestampFunction As PrintEndTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PrintEndTimestampFunction, BigInteger)(printEndTimestampFunction, blockParameter)
        
        End Function

        
        Public Function PrintEndTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of PrintEndTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PrintPhaseStatusQueryAsync(ByVal printPhaseStatusFunction As PrintPhaseStatusFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of PrintPhaseStatusFunction, Boolean)(printPhaseStatusFunction, blockParameter)
        
        End Function

        
        Public Function PrintPhaseStatusQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            return ContractHandler.QueryAsync(Of PrintPhaseStatusFunction, Boolean)(Nothing, blockParameter)
        
        End Function



        Public Function PrintStartTimestampQueryAsync(ByVal printStartTimestampFunction As PrintStartTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PrintStartTimestampFunction, BigInteger)(printStartTimestampFunction, blockParameter)
        
        End Function

        
        Public Function PrintStartTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of PrintStartTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PublicMintEndTimestampQueryAsync(ByVal publicMintEndTimestampFunction As PublicMintEndTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PublicMintEndTimestampFunction, BigInteger)(publicMintEndTimestampFunction, blockParameter)
        
        End Function

        
        Public Function PublicMintEndTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of PublicMintEndTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PublicMintStartTimestampQueryAsync(ByVal publicMintStartTimestampFunction As PublicMintStartTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PublicMintStartTimestampFunction, BigInteger)(publicMintStartTimestampFunction, blockParameter)
        
        End Function

        
        Public Function PublicMintStartTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of PublicMintStartTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PublicMintStatusQueryAsync(ByVal publicMintStatusFunction As PublicMintStatusFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of PublicMintStatusFunction, Boolean)(publicMintStatusFunction, blockParameter)
        
        End Function

        
        Public Function PublicMintStatusQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            return ContractHandler.QueryAsync(Of PublicMintStatusFunction, Boolean)(Nothing, blockParameter)
        
        End Function



        Public Function SignerQueryAsync(ByVal signerFunction As SignerFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of SignerFunction, String)(signerFunction, blockParameter)
        
        End Function

        
        Public Function SignerQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of SignerFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function TreasuryQueryAsync(ByVal treasuryFunction As TreasuryFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of TreasuryFunction, String)(treasuryFunction, blockParameter)
        
        End Function

        
        Public Function TreasuryQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of TreasuryFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function WhitelistMintEndTimestampQueryAsync(ByVal whitelistMintEndTimestampFunction As WhitelistMintEndTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of WhitelistMintEndTimestampFunction, BigInteger)(whitelistMintEndTimestampFunction, blockParameter)
        
        End Function

        
        Public Function WhitelistMintEndTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of WhitelistMintEndTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function WhitelistMintStartTimestampQueryAsync(ByVal whitelistMintStartTimestampFunction As WhitelistMintStartTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of WhitelistMintStartTimestampFunction, BigInteger)(whitelistMintStartTimestampFunction, blockParameter)
        
        End Function

        
        Public Function WhitelistMintStartTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of WhitelistMintStartTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function WhitelistMintStatusQueryAsync(ByVal whitelistMintStatusFunction As WhitelistMintStatusFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of WhitelistMintStatusFunction, Boolean)(whitelistMintStatusFunction, blockParameter)
        
        End Function

        
        Public Function WhitelistMintStatusQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            return ContractHandler.QueryAsync(Of WhitelistMintStatusFunction, Boolean)(Nothing, blockParameter)
        
        End Function



        Public Function WhitelistMintedQueryAsync(ByVal whitelistMintedFunction As WhitelistMintedFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of WhitelistMintedFunction, BigInteger)(whitelistMintedFunction, blockParameter)
        
        End Function

        
        Public Function WhitelistMintedQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim whitelistMintedFunction = New WhitelistMintedFunction()
                whitelistMintedFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of WhitelistMintedFunction, BigInteger)(whitelistMintedFunction, blockParameter)
        
        End Function


    
    End Class

End Namespace
