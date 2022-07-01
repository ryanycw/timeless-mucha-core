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
Imports TimelessMuchaCore.Contracts.TimelessMucha.ContractDefinition
Namespace TimelessMuchaCore.Contracts.TimelessMucha


    Public Partial Class TimelessMuchaService
    
    
        Public Shared Function DeployContractAndWaitForReceiptAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal timelessMuchaDeployment As TimelessMuchaDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return web3.Eth.GetContractDeploymentHandler(Of TimelessMuchaDeployment)().SendRequestAndWaitForReceiptAsync(timelessMuchaDeployment, cancellationTokenSource)
        
        End Function
         Public Shared Function DeployContractAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal timelessMuchaDeployment As TimelessMuchaDeployment) As Task(Of String)
        
            Return web3.Eth.GetContractDeploymentHandler(Of TimelessMuchaDeployment)().SendRequestAsync(timelessMuchaDeployment)
        
        End Function
        Public Shared Async Function DeployContractAndGetServiceAsync(ByVal web3 As Nethereum.Web3.Web3, ByVal timelessMuchaDeployment As TimelessMuchaDeployment, ByVal Optional cancellationTokenSource As CancellationTokenSource = Nothing) As Task(Of TimelessMuchaService)
        
            Dim receipt = Await DeployContractAndWaitForReceiptAsync(web3, timelessMuchaDeployment, cancellationTokenSource)
            Return New TimelessMuchaService(web3, receipt.ContractAddress)
        
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


        Public Function ApproveRequestAsync(ByVal approveFunction As ApproveFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of ApproveFunction)(approveFunction)
        
        End Function

        Public Function ApproveRequestAndWaitForReceiptAsync(ByVal approveFunction As ApproveFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of ApproveFunction)(approveFunction, cancellationToken)
        
        End Function

        
        Public Function ApproveRequestAsync(ByVal [to] As String, ByVal [tokenId] As BigInteger) As Task(Of String)
        
            Dim approveFunction = New ApproveFunction()
                approveFunction.To = [to]
                approveFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAsync(Of ApproveFunction)(approveFunction)
        
        End Function

        
        Public Function ApproveRequestAndWaitForReceiptAsync(ByVal [to] As String, ByVal [tokenId] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim approveFunction = New ApproveFunction()
                approveFunction.To = [to]
                approveFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of ApproveFunction)(approveFunction, cancellationToken)
        
        End Function
        Public Function BalanceOfQueryAsync(ByVal balanceOfFunction As BalanceOfFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of BalanceOfFunction, BigInteger)(balanceOfFunction, blockParameter)
        
        End Function

        
        Public Function BalanceOfQueryAsync(ByVal [owner] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim balanceOfFunction = New BalanceOfFunction()
                balanceOfFunction.Owner = [owner]
            
            Return ContractHandler.QueryAsync(Of BalanceOfFunction, BigInteger)(balanceOfFunction, blockParameter)
        
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


        Public Function GetAllHoldersQueryAsync(ByVal getAllHoldersFunction As GetAllHoldersFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of String))
        
            Return ContractHandler.QueryAsync(Of GetAllHoldersFunction, List(Of String))(getAllHoldersFunction, blockParameter)
        
        End Function

        
        Public Function GetAllHoldersQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of String))
        
            return ContractHandler.QueryAsync(Of GetAllHoldersFunction, List(Of String))(Nothing, blockParameter)
        
        End Function



        Public Function GetAllHoldersInfoQueryAsync(ByVal getAllHoldersInfoFunction As GetAllHoldersInfoFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of GetAllHoldersInfoOutputDTO)
        
            Return ContractHandler.QueryDeserializingToObjectAsync(Of GetAllHoldersInfoFunction, GetAllHoldersInfoOutputDTO)(getAllHoldersInfoFunction, blockParameter)
        
        End Function

        
        Public Function GetAllHoldersInfoQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of GetAllHoldersInfoOutputDTO)
        
            return ContractHandler.QueryDeserializingToObjectAsync(Of GetAllHoldersInfoFunction, GetAllHoldersInfoOutputDTO)(Nothing, blockParameter)
        
        End Function



        Public Function GetApprovedQueryAsync(ByVal getApprovedFunction As GetApprovedFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of GetApprovedFunction, String)(getApprovedFunction, blockParameter)
        
        End Function

        
        Public Function GetApprovedQueryAsync(ByVal [tokenId] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Dim getApprovedFunction = New GetApprovedFunction()
                getApprovedFunction.TokenId = [tokenId]
            
            Return ContractHandler.QueryAsync(Of GetApprovedFunction, String)(getApprovedFunction, blockParameter)
        
        End Function


        Public Function GetDutchAuctionPriceQueryAsync(ByVal getDutchAuctionPriceFunction As GetDutchAuctionPriceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of GetDutchAuctionPriceFunction, BigInteger)(getDutchAuctionPriceFunction, blockParameter)
        
        End Function

        
        Public Function GetDutchAuctionPriceQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of GetDutchAuctionPriceFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function GetTokenPrintStatusQueryAsync(ByVal getTokenPrintStatusFunction As GetTokenPrintStatusFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of GetTokenPrintStatusFunction, Boolean)(getTokenPrintStatusFunction, blockParameter)
        
        End Function

        
        Public Function GetTokenPrintStatusQueryAsync(ByVal [tokenId] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Dim getTokenPrintStatusFunction = New GetTokenPrintStatusFunction()
                getTokenPrintStatusFunction.TokenId = [tokenId]
            
            Return ContractHandler.QueryAsync(Of GetTokenPrintStatusFunction, Boolean)(getTokenPrintStatusFunction, blockParameter)
        
        End Function


        Public Function GetTokenPrintStatusByOwnerQueryAsync(ByVal getTokenPrintStatusByOwnerFunction As GetTokenPrintStatusByOwnerFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of Boolean))
        
            Return ContractHandler.QueryAsync(Of GetTokenPrintStatusByOwnerFunction, List(Of Boolean))(getTokenPrintStatusByOwnerFunction, blockParameter)
        
        End Function

        
        Public Function GetTokenPrintStatusByOwnerQueryAsync(ByVal [owner] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of Boolean))
        
            Dim getTokenPrintStatusByOwnerFunction = New GetTokenPrintStatusByOwnerFunction()
                getTokenPrintStatusByOwnerFunction.Owner = [owner]
            
            Return ContractHandler.QueryAsync(Of GetTokenPrintStatusByOwnerFunction, List(Of Boolean))(getTokenPrintStatusByOwnerFunction, blockParameter)
        
        End Function


        Public Function GetTokenValidStatusQueryAsync(ByVal getTokenValidStatusFunction As GetTokenValidStatusFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of GetTokenValidStatusFunction, Boolean)(getTokenValidStatusFunction, blockParameter)
        
        End Function

        
        Public Function GetTokenValidStatusQueryAsync(ByVal [tokenId] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Dim getTokenValidStatusFunction = New GetTokenValidStatusFunction()
                getTokenValidStatusFunction.TokenId = [tokenId]
            
            Return ContractHandler.QueryAsync(Of GetTokenValidStatusFunction, Boolean)(getTokenValidStatusFunction, blockParameter)
        
        End Function


        Public Function GetTokenValidStatusByOwnerQueryAsync(ByVal getTokenValidStatusByOwnerFunction As GetTokenValidStatusByOwnerFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of Boolean))
        
            Return ContractHandler.QueryAsync(Of GetTokenValidStatusByOwnerFunction, List(Of Boolean))(getTokenValidStatusByOwnerFunction, blockParameter)
        
        End Function

        
        Public Function GetTokenValidStatusByOwnerQueryAsync(ByVal [owner] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of Boolean))
        
            Dim getTokenValidStatusByOwnerFunction = New GetTokenValidStatusByOwnerFunction()
                getTokenValidStatusByOwnerFunction.Owner = [owner]
            
            Return ContractHandler.QueryAsync(Of GetTokenValidStatusByOwnerFunction, List(Of Boolean))(getTokenValidStatusByOwnerFunction, blockParameter)
        
        End Function


        Public Function IsApprovedForAllQueryAsync(ByVal isApprovedForAllFunction As IsApprovedForAllFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of IsApprovedForAllFunction, Boolean)(isApprovedForAllFunction, blockParameter)
        
        End Function

        
        Public Function IsApprovedForAllQueryAsync(ByVal [owner] As String, ByVal [operator] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Dim isApprovedForAllFunction = New IsApprovedForAllFunction()
                isApprovedForAllFunction.Owner = [owner]
                isApprovedForAllFunction.Operator = [operator]
            
            Return ContractHandler.QueryAsync(Of IsApprovedForAllFunction, Boolean)(isApprovedForAllFunction, blockParameter)
        
        End Function


        Public Function IsAuthorizedQueryAsync(ByVal isAuthorizedFunction As IsAuthorizedFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of IsAuthorizedFunction, Boolean)(isAuthorizedFunction, blockParameter)
        
        End Function

        
        Public Function IsAuthorizedQueryAsync(ByVal [returnValue1] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Dim isAuthorizedFunction = New IsAuthorizedFunction()
                isAuthorizedFunction.ReturnValue1 = [returnValue1]
            
            Return ContractHandler.QueryAsync(Of IsAuthorizedFunction, Boolean)(isAuthorizedFunction, blockParameter)
        
        End Function


        Public Function MintDutchAuctionMuchaPapersRequestAsync(ByVal mintDutchAuctionMuchaPapersFunction As MintDutchAuctionMuchaPapersFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of MintDutchAuctionMuchaPapersFunction)(mintDutchAuctionMuchaPapersFunction)
        
        End Function

        Public Function MintDutchAuctionMuchaPapersRequestAndWaitForReceiptAsync(ByVal mintDutchAuctionMuchaPapersFunction As MintDutchAuctionMuchaPapersFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintDutchAuctionMuchaPapersFunction)(mintDutchAuctionMuchaPapersFunction, cancellationToken)
        
        End Function

        
        Public Function MintDutchAuctionMuchaPapersRequestAsync(ByVal [quantities] As BigInteger) As Task(Of String)
        
            Dim mintDutchAuctionMuchaPapersFunction = New MintDutchAuctionMuchaPapersFunction()
                mintDutchAuctionMuchaPapersFunction.Quantities = [quantities]
            
            Return ContractHandler.SendRequestAsync(Of MintDutchAuctionMuchaPapersFunction)(mintDutchAuctionMuchaPapersFunction)
        
        End Function

        
        Public Function MintDutchAuctionMuchaPapersRequestAndWaitForReceiptAsync(ByVal [quantities] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim mintDutchAuctionMuchaPapersFunction = New MintDutchAuctionMuchaPapersFunction()
                mintDutchAuctionMuchaPapersFunction.Quantities = [quantities]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintDutchAuctionMuchaPapersFunction)(mintDutchAuctionMuchaPapersFunction, cancellationToken)
        
        End Function
        Public Function MintGiveawayMuchaPapersRequestAsync(ByVal mintGiveawayMuchaPapersFunction As MintGiveawayMuchaPapersFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of MintGiveawayMuchaPapersFunction)(mintGiveawayMuchaPapersFunction)
        
        End Function

        Public Function MintGiveawayMuchaPapersRequestAndWaitForReceiptAsync(ByVal mintGiveawayMuchaPapersFunction As MintGiveawayMuchaPapersFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintGiveawayMuchaPapersFunction)(mintGiveawayMuchaPapersFunction, cancellationToken)
        
        End Function

        
        Public Function MintGiveawayMuchaPapersRequestAsync(ByVal [to] As String, ByVal [quantities] As BigInteger) As Task(Of String)
        
            Dim mintGiveawayMuchaPapersFunction = New MintGiveawayMuchaPapersFunction()
                mintGiveawayMuchaPapersFunction.To = [to]
                mintGiveawayMuchaPapersFunction.Quantities = [quantities]
            
            Return ContractHandler.SendRequestAsync(Of MintGiveawayMuchaPapersFunction)(mintGiveawayMuchaPapersFunction)
        
        End Function

        
        Public Function MintGiveawayMuchaPapersRequestAndWaitForReceiptAsync(ByVal [to] As String, ByVal [quantities] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim mintGiveawayMuchaPapersFunction = New MintGiveawayMuchaPapersFunction()
                mintGiveawayMuchaPapersFunction.To = [to]
                mintGiveawayMuchaPapersFunction.Quantities = [quantities]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintGiveawayMuchaPapersFunction)(mintGiveawayMuchaPapersFunction, cancellationToken)
        
        End Function
        Public Function MintPublicMuchaPapersRequestAsync(ByVal mintPublicMuchaPapersFunction As MintPublicMuchaPapersFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of MintPublicMuchaPapersFunction)(mintPublicMuchaPapersFunction)
        
        End Function

        Public Function MintPublicMuchaPapersRequestAndWaitForReceiptAsync(ByVal mintPublicMuchaPapersFunction As MintPublicMuchaPapersFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintPublicMuchaPapersFunction)(mintPublicMuchaPapersFunction, cancellationToken)
        
        End Function

        
        Public Function MintPublicMuchaPapersRequestAsync(ByVal [quantities] As BigInteger) As Task(Of String)
        
            Dim mintPublicMuchaPapersFunction = New MintPublicMuchaPapersFunction()
                mintPublicMuchaPapersFunction.Quantities = [quantities]
            
            Return ContractHandler.SendRequestAsync(Of MintPublicMuchaPapersFunction)(mintPublicMuchaPapersFunction)
        
        End Function

        
        Public Function MintPublicMuchaPapersRequestAndWaitForReceiptAsync(ByVal [quantities] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim mintPublicMuchaPapersFunction = New MintPublicMuchaPapersFunction()
                mintPublicMuchaPapersFunction.Quantities = [quantities]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintPublicMuchaPapersFunction)(mintPublicMuchaPapersFunction, cancellationToken)
        
        End Function
        Public Function MintWhitelistMuchaPapersRequestAsync(ByVal mintWhitelistMuchaPapersFunction As MintWhitelistMuchaPapersFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of MintWhitelistMuchaPapersFunction)(mintWhitelistMuchaPapersFunction)
        
        End Function

        Public Function MintWhitelistMuchaPapersRequestAndWaitForReceiptAsync(ByVal mintWhitelistMuchaPapersFunction As MintWhitelistMuchaPapersFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintWhitelistMuchaPapersFunction)(mintWhitelistMuchaPapersFunction, cancellationToken)
        
        End Function

        
        Public Function MintWhitelistMuchaPapersRequestAsync(ByVal [quantities] As BigInteger, ByVal [maxQuantites] As BigInteger, ByVal [signature] As Byte()) As Task(Of String)
        
            Dim mintWhitelistMuchaPapersFunction = New MintWhitelistMuchaPapersFunction()
                mintWhitelistMuchaPapersFunction.Quantities = [quantities]
                mintWhitelistMuchaPapersFunction.MaxQuantites = [maxQuantites]
                mintWhitelistMuchaPapersFunction.Signature = [signature]
            
            Return ContractHandler.SendRequestAsync(Of MintWhitelistMuchaPapersFunction)(mintWhitelistMuchaPapersFunction)
        
        End Function

        
        Public Function MintWhitelistMuchaPapersRequestAndWaitForReceiptAsync(ByVal [quantities] As BigInteger, ByVal [maxQuantites] As BigInteger, ByVal [signature] As Byte(), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim mintWhitelistMuchaPapersFunction = New MintWhitelistMuchaPapersFunction()
                mintWhitelistMuchaPapersFunction.Quantities = [quantities]
                mintWhitelistMuchaPapersFunction.MaxQuantites = [maxQuantites]
                mintWhitelistMuchaPapersFunction.Signature = [signature]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of MintWhitelistMuchaPapersFunction)(mintWhitelistMuchaPapersFunction, cancellationToken)
        
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



        Public Function NameQueryAsync(ByVal nameFunction As NameFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of NameFunction, String)(nameFunction, blockParameter)
        
        End Function

        
        Public Function NameQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of NameFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function OwnerQueryAsync(ByVal ownerFunction As OwnerFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of OwnerFunction, String)(ownerFunction, blockParameter)
        
        End Function

        
        Public Function OwnerQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of OwnerFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function OwnerOfQueryAsync(ByVal ownerOfFunction As OwnerOfFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of OwnerOfFunction, String)(ownerOfFunction, blockParameter)
        
        End Function

        
        Public Function OwnerOfQueryAsync(ByVal [tokenId] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Dim ownerOfFunction = New OwnerOfFunction()
                ownerOfFunction.TokenId = [tokenId]
            
            Return ContractHandler.QueryAsync(Of OwnerOfFunction, String)(ownerOfFunction, blockParameter)
        
        End Function


        Public Function PrintEndTimestampQueryAsync(ByVal printEndTimestampFunction As PrintEndTimestampFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of PrintEndTimestampFunction, BigInteger)(printEndTimestampFunction, blockParameter)
        
        End Function

        
        Public Function PrintEndTimestampQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of PrintEndTimestampFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function PrintMuchaPapersRequestAsync(ByVal printMuchaPapersFunction As PrintMuchaPapersFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of PrintMuchaPapersFunction)(printMuchaPapersFunction)
        
        End Function

        Public Function PrintMuchaPapersRequestAndWaitForReceiptAsync(ByVal printMuchaPapersFunction As PrintMuchaPapersFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of PrintMuchaPapersFunction)(printMuchaPapersFunction, cancellationToken)
        
        End Function

        
        Public Function PrintMuchaPapersRequestAsync(ByVal [tokenId] As BigInteger, ByVal [formId] As BigInteger) As Task(Of String)
        
            Dim printMuchaPapersFunction = New PrintMuchaPapersFunction()
                printMuchaPapersFunction.TokenId = [tokenId]
                printMuchaPapersFunction.FormId = [formId]
            
            Return ContractHandler.SendRequestAsync(Of PrintMuchaPapersFunction)(printMuchaPapersFunction)
        
        End Function

        
        Public Function PrintMuchaPapersRequestAndWaitForReceiptAsync(ByVal [tokenId] As BigInteger, ByVal [formId] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim printMuchaPapersFunction = New PrintMuchaPapersFunction()
                printMuchaPapersFunction.TokenId = [tokenId]
                printMuchaPapersFunction.FormId = [formId]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of PrintMuchaPapersFunction)(printMuchaPapersFunction, cancellationToken)
        
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



        Public Function RenounceOwnershipRequestAsync(ByVal renounceOwnershipFunction As RenounceOwnershipFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of RenounceOwnershipFunction)(renounceOwnershipFunction)
        
        End Function

        Public Function RenounceOwnershipRequestAsync() As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of RenounceOwnershipFunction)
        
        End Function

        Public Function RenounceOwnershipRequestAndWaitForReceiptAsync(ByVal renounceOwnershipFunction As RenounceOwnershipFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of RenounceOwnershipFunction)(renounceOwnershipFunction, cancellationToken)
        
        End Function

        Public Function RenounceOwnershipRequestAndWaitForReceiptAsync(ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of RenounceOwnershipFunction)(Nothing, cancellationToken)
        
        End Function
        Public Function RoyaltyInfoQueryAsync(ByVal royaltyInfoFunction As RoyaltyInfoFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of RoyaltyInfoOutputDTO)
        
            Return ContractHandler.QueryDeserializingToObjectAsync(Of RoyaltyInfoFunction, RoyaltyInfoOutputDTO)(royaltyInfoFunction, blockParameter)
        
        End Function

        
        Public Function RoyaltyInfoQueryAsync(ByVal [tokenId] As BigInteger, ByVal [salePrice] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of RoyaltyInfoOutputDTO)
        
            Dim royaltyInfoFunction = New RoyaltyInfoFunction()
                royaltyInfoFunction.TokenId = [tokenId]
                royaltyInfoFunction.SalePrice = [salePrice]
            
            Return ContractHandler.QueryDeserializingToObjectAsync(Of RoyaltyInfoFunction, RoyaltyInfoOutputDTO)(royaltyInfoFunction, blockParameter)
        
        End Function


        Public Function SafeTransferFromRequestAsync(ByVal safeTransferFromFunction As SafeTransferFromFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SafeTransferFromFunction)(safeTransferFromFunction)
        
        End Function

        Public Function SafeTransferFromRequestAndWaitForReceiptAsync(ByVal safeTransferFromFunction As SafeTransferFromFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SafeTransferFromFunction)(safeTransferFromFunction, cancellationToken)
        
        End Function

        
        Public Function SafeTransferFromRequestAsync(ByVal [from] As String, ByVal [to] As String, ByVal [tokenId] As BigInteger) As Task(Of String)
        
            Dim safeTransferFromFunction = New SafeTransferFromFunction()
                safeTransferFromFunction.From = [from]
                safeTransferFromFunction.To = [to]
                safeTransferFromFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAsync(Of SafeTransferFromFunction)(safeTransferFromFunction)
        
        End Function

        
        Public Function SafeTransferFromRequestAndWaitForReceiptAsync(ByVal [from] As String, ByVal [to] As String, ByVal [tokenId] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim safeTransferFromFunction = New SafeTransferFromFunction()
                safeTransferFromFunction.From = [from]
                safeTransferFromFunction.To = [to]
                safeTransferFromFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SafeTransferFromFunction)(safeTransferFromFunction, cancellationToken)
        
        End Function
        Public Function SafeTransferFromRequestAsync(ByVal safeTransferFrom1Function As SafeTransferFrom1Function) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SafeTransferFrom1Function)(safeTransferFrom1Function)
        
        End Function

        Public Function SafeTransferFromRequestAndWaitForReceiptAsync(ByVal safeTransferFrom1Function As SafeTransferFrom1Function, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SafeTransferFrom1Function)(safeTransferFrom1Function, cancellationToken)
        
        End Function

        
        Public Function SafeTransferFromRequestAsync(ByVal [from] As String, ByVal [to] As String, ByVal [tokenId] As BigInteger, ByVal [data] As Byte()) As Task(Of String)
        
            Dim safeTransferFrom1Function = New SafeTransferFrom1Function()
                safeTransferFrom1Function.From = [from]
                safeTransferFrom1Function.To = [to]
                safeTransferFrom1Function.TokenId = [tokenId]
                safeTransferFrom1Function.Data = [data]
            
            Return ContractHandler.SendRequestAsync(Of SafeTransferFrom1Function)(safeTransferFrom1Function)
        
        End Function

        
        Public Function SafeTransferFromRequestAndWaitForReceiptAsync(ByVal [from] As String, ByVal [to] As String, ByVal [tokenId] As BigInteger, ByVal [data] As Byte(), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim safeTransferFrom1Function = New SafeTransferFrom1Function()
                safeTransferFrom1Function.From = [from]
                safeTransferFrom1Function.To = [to]
                safeTransferFrom1Function.TokenId = [tokenId]
                safeTransferFrom1Function.Data = [data]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SafeTransferFrom1Function)(safeTransferFrom1Function, cancellationToken)
        
        End Function
        Public Function SetApprovalForAllRequestAsync(ByVal setApprovalForAllFunction As SetApprovalForAllFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetApprovalForAllFunction)(setApprovalForAllFunction)
        
        End Function

        Public Function SetApprovalForAllRequestAndWaitForReceiptAsync(ByVal setApprovalForAllFunction As SetApprovalForAllFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetApprovalForAllFunction)(setApprovalForAllFunction, cancellationToken)
        
        End Function

        
        Public Function SetApprovalForAllRequestAsync(ByVal [operator] As String, ByVal [approved] As Boolean) As Task(Of String)
        
            Dim setApprovalForAllFunction = New SetApprovalForAllFunction()
                setApprovalForAllFunction.Operator = [operator]
                setApprovalForAllFunction.Approved = [approved]
            
            Return ContractHandler.SendRequestAsync(Of SetApprovalForAllFunction)(setApprovalForAllFunction)
        
        End Function

        
        Public Function SetApprovalForAllRequestAndWaitForReceiptAsync(ByVal [operator] As String, ByVal [approved] As Boolean, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setApprovalForAllFunction = New SetApprovalForAllFunction()
                setApprovalForAllFunction.Operator = [operator]
                setApprovalForAllFunction.Approved = [approved]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetApprovalForAllFunction)(setApprovalForAllFunction, cancellationToken)
        
        End Function
        Public Function SetAuthorizedRequestAsync(ByVal setAuthorizedFunction As SetAuthorizedFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetAuthorizedFunction)(setAuthorizedFunction)
        
        End Function

        Public Function SetAuthorizedRequestAndWaitForReceiptAsync(ByVal setAuthorizedFunction As SetAuthorizedFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetAuthorizedFunction)(setAuthorizedFunction, cancellationToken)
        
        End Function

        
        Public Function SetAuthorizedRequestAsync(ByVal [newAddress] As String, ByVal [newAuthorizedStatus] As Boolean) As Task(Of String)
        
            Dim setAuthorizedFunction = New SetAuthorizedFunction()
                setAuthorizedFunction.NewAddress = [newAddress]
                setAuthorizedFunction.NewAuthorizedStatus = [newAuthorizedStatus]
            
            Return ContractHandler.SendRequestAsync(Of SetAuthorizedFunction)(setAuthorizedFunction)
        
        End Function

        
        Public Function SetAuthorizedRequestAndWaitForReceiptAsync(ByVal [newAddress] As String, ByVal [newAuthorizedStatus] As Boolean, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setAuthorizedFunction = New SetAuthorizedFunction()
                setAuthorizedFunction.NewAddress = [newAddress]
                setAuthorizedFunction.NewAuthorizedStatus = [newAuthorizedStatus]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetAuthorizedFunction)(setAuthorizedFunction, cancellationToken)
        
        End Function
        Public Function SetAuthorizedInBatchRequestAsync(ByVal setAuthorizedInBatchFunction As SetAuthorizedInBatchFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetAuthorizedInBatchFunction)(setAuthorizedInBatchFunction)
        
        End Function

        Public Function SetAuthorizedInBatchRequestAndWaitForReceiptAsync(ByVal setAuthorizedInBatchFunction As SetAuthorizedInBatchFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetAuthorizedInBatchFunction)(setAuthorizedInBatchFunction, cancellationToken)
        
        End Function

        
        Public Function SetAuthorizedInBatchRequestAsync(ByVal [newAddressArray] As List(Of String), ByVal [newAuthorizedStatusArray] As List(Of Boolean)) As Task(Of String)
        
            Dim setAuthorizedInBatchFunction = New SetAuthorizedInBatchFunction()
                setAuthorizedInBatchFunction.NewAddressArray = [newAddressArray]
                setAuthorizedInBatchFunction.NewAuthorizedStatusArray = [newAuthorizedStatusArray]
            
            Return ContractHandler.SendRequestAsync(Of SetAuthorizedInBatchFunction)(setAuthorizedInBatchFunction)
        
        End Function

        
        Public Function SetAuthorizedInBatchRequestAndWaitForReceiptAsync(ByVal [newAddressArray] As List(Of String), ByVal [newAuthorizedStatusArray] As List(Of Boolean), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setAuthorizedInBatchFunction = New SetAuthorizedInBatchFunction()
                setAuthorizedInBatchFunction.NewAddressArray = [newAddressArray]
                setAuthorizedInBatchFunction.NewAuthorizedStatusArray = [newAuthorizedStatusArray]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetAuthorizedInBatchFunction)(setAuthorizedInBatchFunction, cancellationToken)
        
        End Function
        Public Function SetBaseURIRequestAsync(ByVal setBaseURIFunction As SetBaseURIFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetBaseURIFunction)(setBaseURIFunction)
        
        End Function

        Public Function SetBaseURIRequestAndWaitForReceiptAsync(ByVal setBaseURIFunction As SetBaseURIFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetBaseURIFunction)(setBaseURIFunction, cancellationToken)
        
        End Function

        
        Public Function SetBaseURIRequestAsync(ByVal [newBaseURI] As String) As Task(Of String)
        
            Dim setBaseURIFunction = New SetBaseURIFunction()
                setBaseURIFunction.NewBaseURI = [newBaseURI]
            
            Return ContractHandler.SendRequestAsync(Of SetBaseURIFunction)(setBaseURIFunction)
        
        End Function

        
        Public Function SetBaseURIRequestAndWaitForReceiptAsync(ByVal [newBaseURI] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setBaseURIFunction = New SetBaseURIFunction()
                setBaseURIFunction.NewBaseURI = [newBaseURI]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetBaseURIFunction)(setBaseURIFunction, cancellationToken)
        
        End Function
        Public Function SetDefaultRoyaltyRequestAsync(ByVal setDefaultRoyaltyFunction As SetDefaultRoyaltyFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetDefaultRoyaltyFunction)(setDefaultRoyaltyFunction)
        
        End Function

        Public Function SetDefaultRoyaltyRequestAndWaitForReceiptAsync(ByVal setDefaultRoyaltyFunction As SetDefaultRoyaltyFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetDefaultRoyaltyFunction)(setDefaultRoyaltyFunction, cancellationToken)
        
        End Function

        
        Public Function SetDefaultRoyaltyRequestAsync(ByVal [receiver] As String, ByVal [feeNumerator] As BigInteger) As Task(Of String)
        
            Dim setDefaultRoyaltyFunction = New SetDefaultRoyaltyFunction()
                setDefaultRoyaltyFunction.Receiver = [receiver]
                setDefaultRoyaltyFunction.FeeNumerator = [feeNumerator]
            
            Return ContractHandler.SendRequestAsync(Of SetDefaultRoyaltyFunction)(setDefaultRoyaltyFunction)
        
        End Function

        
        Public Function SetDefaultRoyaltyRequestAndWaitForReceiptAsync(ByVal [receiver] As String, ByVal [feeNumerator] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setDefaultRoyaltyFunction = New SetDefaultRoyaltyFunction()
                setDefaultRoyaltyFunction.Receiver = [receiver]
                setDefaultRoyaltyFunction.FeeNumerator = [feeNumerator]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetDefaultRoyaltyFunction)(setDefaultRoyaltyFunction, cancellationToken)
        
        End Function
        Public Function SetDutchAuctionMintPhaseRequestAsync(ByVal setDutchAuctionMintPhaseFunction As SetDutchAuctionMintPhaseFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetDutchAuctionMintPhaseFunction)(setDutchAuctionMintPhaseFunction)
        
        End Function

        Public Function SetDutchAuctionMintPhaseRequestAndWaitForReceiptAsync(ByVal setDutchAuctionMintPhaseFunction As SetDutchAuctionMintPhaseFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetDutchAuctionMintPhaseFunction)(setDutchAuctionMintPhaseFunction, cancellationToken)
        
        End Function

        
        Public Function SetDutchAuctionMintPhaseRequestAsync(ByVal [newDutchAuctionMintStartTimestamp] As BigInteger, ByVal [newDutchAuctionMintEndTimestamp] As BigInteger, ByVal [newDutchAuctionMintStatus] As Boolean) As Task(Of String)
        
            Dim setDutchAuctionMintPhaseFunction = New SetDutchAuctionMintPhaseFunction()
                setDutchAuctionMintPhaseFunction.NewDutchAuctionMintStartTimestamp = [newDutchAuctionMintStartTimestamp]
                setDutchAuctionMintPhaseFunction.NewDutchAuctionMintEndTimestamp = [newDutchAuctionMintEndTimestamp]
                setDutchAuctionMintPhaseFunction.NewDutchAuctionMintStatus = [newDutchAuctionMintStatus]
            
            Return ContractHandler.SendRequestAsync(Of SetDutchAuctionMintPhaseFunction)(setDutchAuctionMintPhaseFunction)
        
        End Function

        
        Public Function SetDutchAuctionMintPhaseRequestAndWaitForReceiptAsync(ByVal [newDutchAuctionMintStartTimestamp] As BigInteger, ByVal [newDutchAuctionMintEndTimestamp] As BigInteger, ByVal [newDutchAuctionMintStatus] As Boolean, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setDutchAuctionMintPhaseFunction = New SetDutchAuctionMintPhaseFunction()
                setDutchAuctionMintPhaseFunction.NewDutchAuctionMintStartTimestamp = [newDutchAuctionMintStartTimestamp]
                setDutchAuctionMintPhaseFunction.NewDutchAuctionMintEndTimestamp = [newDutchAuctionMintEndTimestamp]
                setDutchAuctionMintPhaseFunction.NewDutchAuctionMintStatus = [newDutchAuctionMintStatus]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetDutchAuctionMintPhaseFunction)(setDutchAuctionMintPhaseFunction, cancellationToken)
        
        End Function
        Public Function SetDutchAuctionSaleInfoRequestAsync(ByVal setDutchAuctionSaleInfoFunction As SetDutchAuctionSaleInfoFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetDutchAuctionSaleInfoFunction)(setDutchAuctionSaleInfoFunction)
        
        End Function

        Public Function SetDutchAuctionSaleInfoRequestAndWaitForReceiptAsync(ByVal setDutchAuctionSaleInfoFunction As SetDutchAuctionSaleInfoFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetDutchAuctionSaleInfoFunction)(setDutchAuctionSaleInfoFunction, cancellationToken)
        
        End Function

        
        Public Function SetDutchAuctionSaleInfoRequestAsync(ByVal [newDutchAuctionTimestep] As BigInteger, ByVal [newDutchAuctionStartPrice] As BigInteger, ByVal [newDutchAuctionEndPrice] As BigInteger, ByVal [newDutchAuctionPriceStep] As BigInteger, ByVal [newDutchAuctionStepAmount] As BigInteger) As Task(Of String)
        
            Dim setDutchAuctionSaleInfoFunction = New SetDutchAuctionSaleInfoFunction()
                setDutchAuctionSaleInfoFunction.NewDutchAuctionTimestep = [newDutchAuctionTimestep]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionStartPrice = [newDutchAuctionStartPrice]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionEndPrice = [newDutchAuctionEndPrice]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionPriceStep = [newDutchAuctionPriceStep]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionStepAmount = [newDutchAuctionStepAmount]
            
            Return ContractHandler.SendRequestAsync(Of SetDutchAuctionSaleInfoFunction)(setDutchAuctionSaleInfoFunction)
        
        End Function

        
        Public Function SetDutchAuctionSaleInfoRequestAndWaitForReceiptAsync(ByVal [newDutchAuctionTimestep] As BigInteger, ByVal [newDutchAuctionStartPrice] As BigInteger, ByVal [newDutchAuctionEndPrice] As BigInteger, ByVal [newDutchAuctionPriceStep] As BigInteger, ByVal [newDutchAuctionStepAmount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setDutchAuctionSaleInfoFunction = New SetDutchAuctionSaleInfoFunction()
                setDutchAuctionSaleInfoFunction.NewDutchAuctionTimestep = [newDutchAuctionTimestep]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionStartPrice = [newDutchAuctionStartPrice]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionEndPrice = [newDutchAuctionEndPrice]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionPriceStep = [newDutchAuctionPriceStep]
                setDutchAuctionSaleInfoFunction.NewDutchAuctionStepAmount = [newDutchAuctionStepAmount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetDutchAuctionSaleInfoFunction)(setDutchAuctionSaleInfoFunction, cancellationToken)
        
        End Function
        Public Function SetFormMaxLimitRequestAsync(ByVal setFormMaxLimitFunction As SetFormMaxLimitFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetFormMaxLimitFunction)(setFormMaxLimitFunction)
        
        End Function

        Public Function SetFormMaxLimitRequestAndWaitForReceiptAsync(ByVal setFormMaxLimitFunction As SetFormMaxLimitFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetFormMaxLimitFunction)(setFormMaxLimitFunction, cancellationToken)
        
        End Function

        
        Public Function SetFormMaxLimitRequestAsync(ByVal [id] As BigInteger, ByVal [maxAmount] As BigInteger) As Task(Of String)
        
            Dim setFormMaxLimitFunction = New SetFormMaxLimitFunction()
                setFormMaxLimitFunction.Id = [id]
                setFormMaxLimitFunction.MaxAmount = [maxAmount]
            
            Return ContractHandler.SendRequestAsync(Of SetFormMaxLimitFunction)(setFormMaxLimitFunction)
        
        End Function

        
        Public Function SetFormMaxLimitRequestAndWaitForReceiptAsync(ByVal [id] As BigInteger, ByVal [maxAmount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setFormMaxLimitFunction = New SetFormMaxLimitFunction()
                setFormMaxLimitFunction.Id = [id]
                setFormMaxLimitFunction.MaxAmount = [maxAmount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetFormMaxLimitFunction)(setFormMaxLimitFunction, cancellationToken)
        
        End Function
        Public Function SetFormMaxLimitInBatchRequestAsync(ByVal setFormMaxLimitInBatchFunction As SetFormMaxLimitInBatchFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetFormMaxLimitInBatchFunction)(setFormMaxLimitInBatchFunction)
        
        End Function

        Public Function SetFormMaxLimitInBatchRequestAndWaitForReceiptAsync(ByVal setFormMaxLimitInBatchFunction As SetFormMaxLimitInBatchFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetFormMaxLimitInBatchFunction)(setFormMaxLimitInBatchFunction, cancellationToken)
        
        End Function

        
        Public Function SetFormMaxLimitInBatchRequestAsync(ByVal [ids] As List(Of BigInteger), ByVal [maxAmounts] As List(Of BigInteger)) As Task(Of String)
        
            Dim setFormMaxLimitInBatchFunction = New SetFormMaxLimitInBatchFunction()
                setFormMaxLimitInBatchFunction.Ids = [ids]
                setFormMaxLimitInBatchFunction.MaxAmounts = [maxAmounts]
            
            Return ContractHandler.SendRequestAsync(Of SetFormMaxLimitInBatchFunction)(setFormMaxLimitInBatchFunction)
        
        End Function

        
        Public Function SetFormMaxLimitInBatchRequestAndWaitForReceiptAsync(ByVal [ids] As List(Of BigInteger), ByVal [maxAmounts] As List(Of BigInteger), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setFormMaxLimitInBatchFunction = New SetFormMaxLimitInBatchFunction()
                setFormMaxLimitInBatchFunction.Ids = [ids]
                setFormMaxLimitInBatchFunction.MaxAmounts = [maxAmounts]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetFormMaxLimitInBatchFunction)(setFormMaxLimitInBatchFunction, cancellationToken)
        
        End Function
        Public Function SetMaxMuchaPapersAmountRequestAsync(ByVal setMaxMuchaPapersAmountFunction As SetMaxMuchaPapersAmountFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetMaxMuchaPapersAmountFunction)(setMaxMuchaPapersAmountFunction)
        
        End Function

        Public Function SetMaxMuchaPapersAmountRequestAndWaitForReceiptAsync(ByVal setMaxMuchaPapersAmountFunction As SetMaxMuchaPapersAmountFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMaxMuchaPapersAmountFunction)(setMaxMuchaPapersAmountFunction, cancellationToken)
        
        End Function

        
        Public Function SetMaxMuchaPapersAmountRequestAsync(ByVal [newMaxMuchaPapersAmount] As BigInteger) As Task(Of String)
        
            Dim setMaxMuchaPapersAmountFunction = New SetMaxMuchaPapersAmountFunction()
                setMaxMuchaPapersAmountFunction.NewMaxMuchaPapersAmount = [newMaxMuchaPapersAmount]
            
            Return ContractHandler.SendRequestAsync(Of SetMaxMuchaPapersAmountFunction)(setMaxMuchaPapersAmountFunction)
        
        End Function

        
        Public Function SetMaxMuchaPapersAmountRequestAndWaitForReceiptAsync(ByVal [newMaxMuchaPapersAmount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setMaxMuchaPapersAmountFunction = New SetMaxMuchaPapersAmountFunction()
                setMaxMuchaPapersAmountFunction.NewMaxMuchaPapersAmount = [newMaxMuchaPapersAmount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMaxMuchaPapersAmountFunction)(setMaxMuchaPapersAmountFunction, cancellationToken)
        
        End Function
        Public Function SetMaxMuchaPapersPerTxRequestAsync(ByVal setMaxMuchaPapersPerTxFunction As SetMaxMuchaPapersPerTxFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetMaxMuchaPapersPerTxFunction)(setMaxMuchaPapersPerTxFunction)
        
        End Function

        Public Function SetMaxMuchaPapersPerTxRequestAndWaitForReceiptAsync(ByVal setMaxMuchaPapersPerTxFunction As SetMaxMuchaPapersPerTxFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMaxMuchaPapersPerTxFunction)(setMaxMuchaPapersPerTxFunction, cancellationToken)
        
        End Function

        
        Public Function SetMaxMuchaPapersPerTxRequestAsync(ByVal [newMaxMuchaPapersPerTx] As BigInteger) As Task(Of String)
        
            Dim setMaxMuchaPapersPerTxFunction = New SetMaxMuchaPapersPerTxFunction()
                setMaxMuchaPapersPerTxFunction.NewMaxMuchaPapersPerTx = [newMaxMuchaPapersPerTx]
            
            Return ContractHandler.SendRequestAsync(Of SetMaxMuchaPapersPerTxFunction)(setMaxMuchaPapersPerTxFunction)
        
        End Function

        
        Public Function SetMaxMuchaPapersPerTxRequestAndWaitForReceiptAsync(ByVal [newMaxMuchaPapersPerTx] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setMaxMuchaPapersPerTxFunction = New SetMaxMuchaPapersPerTxFunction()
                setMaxMuchaPapersPerTxFunction.NewMaxMuchaPapersPerTx = [newMaxMuchaPapersPerTx]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMaxMuchaPapersPerTxFunction)(setMaxMuchaPapersPerTxFunction, cancellationToken)
        
        End Function
        Public Function SetMaxMuchaPapersTierAmountRequestAsync(ByVal setMaxMuchaPapersTierAmountFunction As SetMaxMuchaPapersTierAmountFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetMaxMuchaPapersTierAmountFunction)(setMaxMuchaPapersTierAmountFunction)
        
        End Function

        Public Function SetMaxMuchaPapersTierAmountRequestAndWaitForReceiptAsync(ByVal setMaxMuchaPapersTierAmountFunction As SetMaxMuchaPapersTierAmountFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMaxMuchaPapersTierAmountFunction)(setMaxMuchaPapersTierAmountFunction, cancellationToken)
        
        End Function

        
        Public Function SetMaxMuchaPapersTierAmountRequestAsync(ByVal [newMaxMuchaPapersTierAmount] As BigInteger) As Task(Of String)
        
            Dim setMaxMuchaPapersTierAmountFunction = New SetMaxMuchaPapersTierAmountFunction()
                setMaxMuchaPapersTierAmountFunction.NewMaxMuchaPapersTierAmount = [newMaxMuchaPapersTierAmount]
            
            Return ContractHandler.SendRequestAsync(Of SetMaxMuchaPapersTierAmountFunction)(setMaxMuchaPapersTierAmountFunction)
        
        End Function

        
        Public Function SetMaxMuchaPapersTierAmountRequestAndWaitForReceiptAsync(ByVal [newMaxMuchaPapersTierAmount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setMaxMuchaPapersTierAmountFunction = New SetMaxMuchaPapersTierAmountFunction()
                setMaxMuchaPapersTierAmountFunction.NewMaxMuchaPapersTierAmount = [newMaxMuchaPapersTierAmount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMaxMuchaPapersTierAmountFunction)(setMaxMuchaPapersTierAmountFunction, cancellationToken)
        
        End Function
        Public Function SetMuchaPapersPriceRequestAsync(ByVal setMuchaPapersPriceFunction As SetMuchaPapersPriceFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetMuchaPapersPriceFunction)(setMuchaPapersPriceFunction)
        
        End Function

        Public Function SetMuchaPapersPriceRequestAndWaitForReceiptAsync(ByVal setMuchaPapersPriceFunction As SetMuchaPapersPriceFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMuchaPapersPriceFunction)(setMuchaPapersPriceFunction, cancellationToken)
        
        End Function

        
        Public Function SetMuchaPapersPriceRequestAsync(ByVal [newPrice] As BigInteger) As Task(Of String)
        
            Dim setMuchaPapersPriceFunction = New SetMuchaPapersPriceFunction()
                setMuchaPapersPriceFunction.NewPrice = [newPrice]
            
            Return ContractHandler.SendRequestAsync(Of SetMuchaPapersPriceFunction)(setMuchaPapersPriceFunction)
        
        End Function

        
        Public Function SetMuchaPapersPriceRequestAndWaitForReceiptAsync(ByVal [newPrice] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setMuchaPapersPriceFunction = New SetMuchaPapersPriceFunction()
                setMuchaPapersPriceFunction.NewPrice = [newPrice]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetMuchaPapersPriceFunction)(setMuchaPapersPriceFunction, cancellationToken)
        
        End Function
        Public Function SetPrintPhaseRequestAsync(ByVal setPrintPhaseFunction As SetPrintPhaseFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetPrintPhaseFunction)(setPrintPhaseFunction)
        
        End Function

        Public Function SetPrintPhaseRequestAndWaitForReceiptAsync(ByVal setPrintPhaseFunction As SetPrintPhaseFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetPrintPhaseFunction)(setPrintPhaseFunction, cancellationToken)
        
        End Function

        
        Public Function SetPrintPhaseRequestAsync(ByVal [newPrintStartTimestamp] As BigInteger, ByVal [newPrintEndTimestamp] As BigInteger, ByVal [newPrintEnableStatus] As Boolean) As Task(Of String)
        
            Dim setPrintPhaseFunction = New SetPrintPhaseFunction()
                setPrintPhaseFunction.NewPrintStartTimestamp = [newPrintStartTimestamp]
                setPrintPhaseFunction.NewPrintEndTimestamp = [newPrintEndTimestamp]
                setPrintPhaseFunction.NewPrintEnableStatus = [newPrintEnableStatus]
            
            Return ContractHandler.SendRequestAsync(Of SetPrintPhaseFunction)(setPrintPhaseFunction)
        
        End Function

        
        Public Function SetPrintPhaseRequestAndWaitForReceiptAsync(ByVal [newPrintStartTimestamp] As BigInteger, ByVal [newPrintEndTimestamp] As BigInteger, ByVal [newPrintEnableStatus] As Boolean, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setPrintPhaseFunction = New SetPrintPhaseFunction()
                setPrintPhaseFunction.NewPrintStartTimestamp = [newPrintStartTimestamp]
                setPrintPhaseFunction.NewPrintEndTimestamp = [newPrintEndTimestamp]
                setPrintPhaseFunction.NewPrintEnableStatus = [newPrintEnableStatus]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetPrintPhaseFunction)(setPrintPhaseFunction, cancellationToken)
        
        End Function
        Public Function SetPublicMintPhaseRequestAsync(ByVal setPublicMintPhaseFunction As SetPublicMintPhaseFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetPublicMintPhaseFunction)(setPublicMintPhaseFunction)
        
        End Function

        Public Function SetPublicMintPhaseRequestAndWaitForReceiptAsync(ByVal setPublicMintPhaseFunction As SetPublicMintPhaseFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetPublicMintPhaseFunction)(setPublicMintPhaseFunction, cancellationToken)
        
        End Function

        
        Public Function SetPublicMintPhaseRequestAsync(ByVal [newPublicMintStartTimestamp] As BigInteger, ByVal [newPublicMintEndTimestamp] As BigInteger, ByVal [newPublicMintEnableStatus] As Boolean) As Task(Of String)
        
            Dim setPublicMintPhaseFunction = New SetPublicMintPhaseFunction()
                setPublicMintPhaseFunction.NewPublicMintStartTimestamp = [newPublicMintStartTimestamp]
                setPublicMintPhaseFunction.NewPublicMintEndTimestamp = [newPublicMintEndTimestamp]
                setPublicMintPhaseFunction.NewPublicMintEnableStatus = [newPublicMintEnableStatus]
            
            Return ContractHandler.SendRequestAsync(Of SetPublicMintPhaseFunction)(setPublicMintPhaseFunction)
        
        End Function

        
        Public Function SetPublicMintPhaseRequestAndWaitForReceiptAsync(ByVal [newPublicMintStartTimestamp] As BigInteger, ByVal [newPublicMintEndTimestamp] As BigInteger, ByVal [newPublicMintEnableStatus] As Boolean, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setPublicMintPhaseFunction = New SetPublicMintPhaseFunction()
                setPublicMintPhaseFunction.NewPublicMintStartTimestamp = [newPublicMintStartTimestamp]
                setPublicMintPhaseFunction.NewPublicMintEndTimestamp = [newPublicMintEndTimestamp]
                setPublicMintPhaseFunction.NewPublicMintEnableStatus = [newPublicMintEnableStatus]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetPublicMintPhaseFunction)(setPublicMintPhaseFunction, cancellationToken)
        
        End Function
        Public Function SetSignerRequestAsync(ByVal setSignerFunction As SetSignerFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetSignerFunction)(setSignerFunction)
        
        End Function

        Public Function SetSignerRequestAndWaitForReceiptAsync(ByVal setSignerFunction As SetSignerFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetSignerFunction)(setSignerFunction, cancellationToken)
        
        End Function

        
        Public Function SetSignerRequestAsync(ByVal [newSignerAddress] As String) As Task(Of String)
        
            Dim setSignerFunction = New SetSignerFunction()
                setSignerFunction.NewSignerAddress = [newSignerAddress]
            
            Return ContractHandler.SendRequestAsync(Of SetSignerFunction)(setSignerFunction)
        
        End Function

        
        Public Function SetSignerRequestAndWaitForReceiptAsync(ByVal [newSignerAddress] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setSignerFunction = New SetSignerFunction()
                setSignerFunction.NewSignerAddress = [newSignerAddress]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetSignerFunction)(setSignerFunction, cancellationToken)
        
        End Function
        Public Function SetTokenInvalidRequestAsync(ByVal setTokenInvalidFunction As SetTokenInvalidFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetTokenInvalidFunction)(setTokenInvalidFunction)
        
        End Function

        Public Function SetTokenInvalidRequestAndWaitForReceiptAsync(ByVal setTokenInvalidFunction As SetTokenInvalidFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenInvalidFunction)(setTokenInvalidFunction, cancellationToken)
        
        End Function

        
        Public Function SetTokenInvalidRequestAsync(ByVal [tokenId] As BigInteger) As Task(Of String)
        
            Dim setTokenInvalidFunction = New SetTokenInvalidFunction()
                setTokenInvalidFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAsync(Of SetTokenInvalidFunction)(setTokenInvalidFunction)
        
        End Function

        
        Public Function SetTokenInvalidRequestAndWaitForReceiptAsync(ByVal [tokenId] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setTokenInvalidFunction = New SetTokenInvalidFunction()
                setTokenInvalidFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenInvalidFunction)(setTokenInvalidFunction, cancellationToken)
        
        End Function
        Public Function SetTokenInvalidInBatchRequestAsync(ByVal setTokenInvalidInBatchFunction As SetTokenInvalidInBatchFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetTokenInvalidInBatchFunction)(setTokenInvalidInBatchFunction)
        
        End Function

        Public Function SetTokenInvalidInBatchRequestAndWaitForReceiptAsync(ByVal setTokenInvalidInBatchFunction As SetTokenInvalidInBatchFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenInvalidInBatchFunction)(setTokenInvalidInBatchFunction, cancellationToken)
        
        End Function

        
        Public Function SetTokenInvalidInBatchRequestAsync(ByVal [tokenIds] As List(Of BigInteger)) As Task(Of String)
        
            Dim setTokenInvalidInBatchFunction = New SetTokenInvalidInBatchFunction()
                setTokenInvalidInBatchFunction.TokenIds = [tokenIds]
            
            Return ContractHandler.SendRequestAsync(Of SetTokenInvalidInBatchFunction)(setTokenInvalidInBatchFunction)
        
        End Function

        
        Public Function SetTokenInvalidInBatchRequestAndWaitForReceiptAsync(ByVal [tokenIds] As List(Of BigInteger), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setTokenInvalidInBatchFunction = New SetTokenInvalidInBatchFunction()
                setTokenInvalidInBatchFunction.TokenIds = [tokenIds]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenInvalidInBatchFunction)(setTokenInvalidInBatchFunction, cancellationToken)
        
        End Function
        Public Function SetTokenRoyaltyRequestAsync(ByVal setTokenRoyaltyFunction As SetTokenRoyaltyFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetTokenRoyaltyFunction)(setTokenRoyaltyFunction)
        
        End Function

        Public Function SetTokenRoyaltyRequestAndWaitForReceiptAsync(ByVal setTokenRoyaltyFunction As SetTokenRoyaltyFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenRoyaltyFunction)(setTokenRoyaltyFunction, cancellationToken)
        
        End Function

        
        Public Function SetTokenRoyaltyRequestAsync(ByVal [tokenId] As BigInteger, ByVal [receiver] As String, ByVal [feeNumerator] As BigInteger) As Task(Of String)
        
            Dim setTokenRoyaltyFunction = New SetTokenRoyaltyFunction()
                setTokenRoyaltyFunction.TokenId = [tokenId]
                setTokenRoyaltyFunction.Receiver = [receiver]
                setTokenRoyaltyFunction.FeeNumerator = [feeNumerator]
            
            Return ContractHandler.SendRequestAsync(Of SetTokenRoyaltyFunction)(setTokenRoyaltyFunction)
        
        End Function

        
        Public Function SetTokenRoyaltyRequestAndWaitForReceiptAsync(ByVal [tokenId] As BigInteger, ByVal [receiver] As String, ByVal [feeNumerator] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setTokenRoyaltyFunction = New SetTokenRoyaltyFunction()
                setTokenRoyaltyFunction.TokenId = [tokenId]
                setTokenRoyaltyFunction.Receiver = [receiver]
                setTokenRoyaltyFunction.FeeNumerator = [feeNumerator]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenRoyaltyFunction)(setTokenRoyaltyFunction, cancellationToken)
        
        End Function
        Public Function SetTokenValidRequestAsync(ByVal setTokenValidFunction As SetTokenValidFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetTokenValidFunction)(setTokenValidFunction)
        
        End Function

        Public Function SetTokenValidRequestAndWaitForReceiptAsync(ByVal setTokenValidFunction As SetTokenValidFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenValidFunction)(setTokenValidFunction, cancellationToken)
        
        End Function

        
        Public Function SetTokenValidRequestAsync(ByVal [tokenId] As BigInteger) As Task(Of String)
        
            Dim setTokenValidFunction = New SetTokenValidFunction()
                setTokenValidFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAsync(Of SetTokenValidFunction)(setTokenValidFunction)
        
        End Function

        
        Public Function SetTokenValidRequestAndWaitForReceiptAsync(ByVal [tokenId] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setTokenValidFunction = New SetTokenValidFunction()
                setTokenValidFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenValidFunction)(setTokenValidFunction, cancellationToken)
        
        End Function
        Public Function SetTokenValidInBatchRequestAsync(ByVal setTokenValidInBatchFunction As SetTokenValidInBatchFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetTokenValidInBatchFunction)(setTokenValidInBatchFunction)
        
        End Function

        Public Function SetTokenValidInBatchRequestAndWaitForReceiptAsync(ByVal setTokenValidInBatchFunction As SetTokenValidInBatchFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenValidInBatchFunction)(setTokenValidInBatchFunction, cancellationToken)
        
        End Function

        
        Public Function SetTokenValidInBatchRequestAsync(ByVal [tokenIds] As List(Of BigInteger)) As Task(Of String)
        
            Dim setTokenValidInBatchFunction = New SetTokenValidInBatchFunction()
                setTokenValidInBatchFunction.TokenIds = [tokenIds]
            
            Return ContractHandler.SendRequestAsync(Of SetTokenValidInBatchFunction)(setTokenValidInBatchFunction)
        
        End Function

        
        Public Function SetTokenValidInBatchRequestAndWaitForReceiptAsync(ByVal [tokenIds] As List(Of BigInteger), ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setTokenValidInBatchFunction = New SetTokenValidInBatchFunction()
                setTokenValidInBatchFunction.TokenIds = [tokenIds]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTokenValidInBatchFunction)(setTokenValidInBatchFunction, cancellationToken)
        
        End Function
        Public Function SetTreasuryRequestAsync(ByVal setTreasuryFunction As SetTreasuryFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetTreasuryFunction)(setTreasuryFunction)
        
        End Function

        Public Function SetTreasuryRequestAndWaitForReceiptAsync(ByVal setTreasuryFunction As SetTreasuryFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTreasuryFunction)(setTreasuryFunction, cancellationToken)
        
        End Function

        
        Public Function SetTreasuryRequestAsync(ByVal [newTreasuryAddress] As String) As Task(Of String)
        
            Dim setTreasuryFunction = New SetTreasuryFunction()
                setTreasuryFunction.NewTreasuryAddress = [newTreasuryAddress]
            
            Return ContractHandler.SendRequestAsync(Of SetTreasuryFunction)(setTreasuryFunction)
        
        End Function

        
        Public Function SetTreasuryRequestAndWaitForReceiptAsync(ByVal [newTreasuryAddress] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setTreasuryFunction = New SetTreasuryFunction()
                setTreasuryFunction.NewTreasuryAddress = [newTreasuryAddress]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetTreasuryFunction)(setTreasuryFunction, cancellationToken)
        
        End Function
        Public Function SetWhitelistMintPhaseRequestAsync(ByVal setWhitelistMintPhaseFunction As SetWhitelistMintPhaseFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of SetWhitelistMintPhaseFunction)(setWhitelistMintPhaseFunction)
        
        End Function

        Public Function SetWhitelistMintPhaseRequestAndWaitForReceiptAsync(ByVal setWhitelistMintPhaseFunction As SetWhitelistMintPhaseFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetWhitelistMintPhaseFunction)(setWhitelistMintPhaseFunction, cancellationToken)
        
        End Function

        
        Public Function SetWhitelistMintPhaseRequestAsync(ByVal [newWhitelistMintStartTimestamp] As BigInteger, ByVal [newWhitelistMintEndTimestamp] As BigInteger, ByVal [newWhitelistMintEnableStatus] As Boolean) As Task(Of String)
        
            Dim setWhitelistMintPhaseFunction = New SetWhitelistMintPhaseFunction()
                setWhitelistMintPhaseFunction.NewWhitelistMintStartTimestamp = [newWhitelistMintStartTimestamp]
                setWhitelistMintPhaseFunction.NewWhitelistMintEndTimestamp = [newWhitelistMintEndTimestamp]
                setWhitelistMintPhaseFunction.NewWhitelistMintEnableStatus = [newWhitelistMintEnableStatus]
            
            Return ContractHandler.SendRequestAsync(Of SetWhitelistMintPhaseFunction)(setWhitelistMintPhaseFunction)
        
        End Function

        
        Public Function SetWhitelistMintPhaseRequestAndWaitForReceiptAsync(ByVal [newWhitelistMintStartTimestamp] As BigInteger, ByVal [newWhitelistMintEndTimestamp] As BigInteger, ByVal [newWhitelistMintEnableStatus] As Boolean, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim setWhitelistMintPhaseFunction = New SetWhitelistMintPhaseFunction()
                setWhitelistMintPhaseFunction.NewWhitelistMintStartTimestamp = [newWhitelistMintStartTimestamp]
                setWhitelistMintPhaseFunction.NewWhitelistMintEndTimestamp = [newWhitelistMintEndTimestamp]
                setWhitelistMintPhaseFunction.NewWhitelistMintEnableStatus = [newWhitelistMintEnableStatus]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of SetWhitelistMintPhaseFunction)(setWhitelistMintPhaseFunction, cancellationToken)
        
        End Function
        Public Function SignerQueryAsync(ByVal signerFunction As SignerFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of SignerFunction, String)(signerFunction, blockParameter)
        
        End Function

        
        Public Function SignerQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of SignerFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function SupportsInterfaceQueryAsync(ByVal supportsInterfaceFunction As SupportsInterfaceFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Return ContractHandler.QueryAsync(Of SupportsInterfaceFunction, Boolean)(supportsInterfaceFunction, blockParameter)
        
        End Function

        
        Public Function SupportsInterfaceQueryAsync(ByVal [interfaceId] As Byte(), ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of Boolean)
        
            Dim supportsInterfaceFunction = New SupportsInterfaceFunction()
                supportsInterfaceFunction.InterfaceId = [interfaceId]
            
            Return ContractHandler.QueryAsync(Of SupportsInterfaceFunction, Boolean)(supportsInterfaceFunction, blockParameter)
        
        End Function


        Public Function SymbolQueryAsync(ByVal symbolFunction As SymbolFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of SymbolFunction, String)(symbolFunction, blockParameter)
        
        End Function

        
        Public Function SymbolQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            return ContractHandler.QueryAsync(Of SymbolFunction, String)(Nothing, blockParameter)
        
        End Function



        Public Function TokenByIndexQueryAsync(ByVal tokenByIndexFunction As TokenByIndexFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of TokenByIndexFunction, BigInteger)(tokenByIndexFunction, blockParameter)
        
        End Function

        
        Public Function TokenByIndexQueryAsync(ByVal [index] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim tokenByIndexFunction = New TokenByIndexFunction()
                tokenByIndexFunction.Index = [index]
            
            Return ContractHandler.QueryAsync(Of TokenByIndexFunction, BigInteger)(tokenByIndexFunction, blockParameter)
        
        End Function


        Public Function TokenOfOwnerByIndexQueryAsync(ByVal tokenOfOwnerByIndexFunction As TokenOfOwnerByIndexFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of TokenOfOwnerByIndexFunction, BigInteger)(tokenOfOwnerByIndexFunction, blockParameter)
        
        End Function

        
        Public Function TokenOfOwnerByIndexQueryAsync(ByVal [owner] As String, ByVal [index] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Dim tokenOfOwnerByIndexFunction = New TokenOfOwnerByIndexFunction()
                tokenOfOwnerByIndexFunction.Owner = [owner]
                tokenOfOwnerByIndexFunction.Index = [index]
            
            Return ContractHandler.QueryAsync(Of TokenOfOwnerByIndexFunction, BigInteger)(tokenOfOwnerByIndexFunction, blockParameter)
        
        End Function


        Public Function TokenURIQueryAsync(ByVal tokenURIFunction As TokenURIFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Return ContractHandler.QueryAsync(Of TokenURIFunction, String)(tokenURIFunction, blockParameter)
        
        End Function

        
        Public Function TokenURIQueryAsync(ByVal [tokenId] As BigInteger, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of String)
        
            Dim tokenURIFunction = New TokenURIFunction()
                tokenURIFunction.TokenId = [tokenId]
            
            Return ContractHandler.QueryAsync(Of TokenURIFunction, String)(tokenURIFunction, blockParameter)
        
        End Function


        Public Function TokensOfOwnerQueryAsync(ByVal tokensOfOwnerFunction As TokensOfOwnerFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of BigInteger))
        
            Return ContractHandler.QueryAsync(Of TokensOfOwnerFunction, List(Of BigInteger))(tokensOfOwnerFunction, blockParameter)
        
        End Function

        
        Public Function TokensOfOwnerQueryAsync(ByVal [owner] As String, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of List(Of BigInteger))
        
            Dim tokensOfOwnerFunction = New TokensOfOwnerFunction()
                tokensOfOwnerFunction.Owner = [owner]
            
            Return ContractHandler.QueryAsync(Of TokensOfOwnerFunction, List(Of BigInteger))(tokensOfOwnerFunction, blockParameter)
        
        End Function


        Public Function TotalSupplyQueryAsync(ByVal totalSupplyFunction As TotalSupplyFunction, ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            Return ContractHandler.QueryAsync(Of TotalSupplyFunction, BigInteger)(totalSupplyFunction, blockParameter)
        
        End Function

        
        Public Function TotalSupplyQueryAsync(ByVal Optional blockParameter As BlockParameter = Nothing) As Task(Of BigInteger)
        
            return ContractHandler.QueryAsync(Of TotalSupplyFunction, BigInteger)(Nothing, blockParameter)
        
        End Function



        Public Function TransferFromRequestAsync(ByVal transferFromFunction As TransferFromFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of TransferFromFunction)(transferFromFunction)
        
        End Function

        Public Function TransferFromRequestAndWaitForReceiptAsync(ByVal transferFromFunction As TransferFromFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferFromFunction)(transferFromFunction, cancellationToken)
        
        End Function

        
        Public Function TransferFromRequestAsync(ByVal [from] As String, ByVal [to] As String, ByVal [tokenId] As BigInteger) As Task(Of String)
        
            Dim transferFromFunction = New TransferFromFunction()
                transferFromFunction.From = [from]
                transferFromFunction.To = [to]
                transferFromFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAsync(Of TransferFromFunction)(transferFromFunction)
        
        End Function

        
        Public Function TransferFromRequestAndWaitForReceiptAsync(ByVal [from] As String, ByVal [to] As String, ByVal [tokenId] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim transferFromFunction = New TransferFromFunction()
                transferFromFunction.From = [from]
                transferFromFunction.To = [to]
                transferFromFunction.TokenId = [tokenId]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferFromFunction)(transferFromFunction, cancellationToken)
        
        End Function
        Public Function TransferOwnershipRequestAsync(ByVal transferOwnershipFunction As TransferOwnershipFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of TransferOwnershipFunction)(transferOwnershipFunction)
        
        End Function

        Public Function TransferOwnershipRequestAndWaitForReceiptAsync(ByVal transferOwnershipFunction As TransferOwnershipFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferOwnershipFunction)(transferOwnershipFunction, cancellationToken)
        
        End Function

        
        Public Function TransferOwnershipRequestAsync(ByVal [newOwner] As String) As Task(Of String)
        
            Dim transferOwnershipFunction = New TransferOwnershipFunction()
                transferOwnershipFunction.NewOwner = [newOwner]
            
            Return ContractHandler.SendRequestAsync(Of TransferOwnershipFunction)(transferOwnershipFunction)
        
        End Function

        
        Public Function TransferOwnershipRequestAndWaitForReceiptAsync(ByVal [newOwner] As String, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim transferOwnershipFunction = New TransferOwnershipFunction()
                transferOwnershipFunction.NewOwner = [newOwner]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of TransferOwnershipFunction)(transferOwnershipFunction, cancellationToken)
        
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


        Public Function WithdrawRequestAsync(ByVal withdrawFunction As WithdrawFunction) As Task(Of String)
                    
            Return ContractHandler.SendRequestAsync(Of WithdrawFunction)(withdrawFunction)
        
        End Function

        Public Function WithdrawRequestAndWaitForReceiptAsync(ByVal withdrawFunction As WithdrawFunction, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of WithdrawFunction)(withdrawFunction, cancellationToken)
        
        End Function

        
        Public Function WithdrawRequestAsync(ByVal [amount] As BigInteger) As Task(Of String)
        
            Dim withdrawFunction = New WithdrawFunction()
                withdrawFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAsync(Of WithdrawFunction)(withdrawFunction)
        
        End Function

        
        Public Function WithdrawRequestAndWaitForReceiptAsync(ByVal [amount] As BigInteger, ByVal Optional cancellationToken As CancellationTokenSource = Nothing) As Task(Of TransactionReceipt)
        
            Dim withdrawFunction = New WithdrawFunction()
                withdrawFunction.Amount = [amount]
            
            Return ContractHandler.SendRequestAndWaitForReceiptAsync(Of WithdrawFunction)(withdrawFunction, cancellationToken)
        
        End Function
    
    End Class

End Namespace
