Imports System
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Numerics
Imports Nethereum.Hex.HexTypes
Imports Nethereum.ABI.FunctionEncoding.Attributes
Namespace TimelessMuchaCore.Contracts.TimelessMucha.ContractDefinition

    Public Partial Class HolderInfo
        Inherits HolderInfoBase
    End Class

    Public Class HolderInfoBase
        
        <[Parameter]("address", "holderAddress", 1)>
        Public Overridable Property [HolderAddress] As String
        <[Parameter]("uint256", "balance", 2)>
        Public Overridable Property [Balance] As BigInteger
        <[Parameter]("uint256[]", "tokenIds", 3)>
        Public Overridable Property [TokenIds] As List(Of BigInteger)
    
    End Class

End Namespace
