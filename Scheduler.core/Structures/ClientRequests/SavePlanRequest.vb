﻿Imports System
Imports System.Collections.Generic
Imports System.Runtime.Serialization

<DataContract()>
Public Class SavePlanRequest
    <DataMember()>
    Public Property PlanData As List(Of PlanItem)
    <DataMember()>
    Public Property LineData As Line
    <DataMember()>
    Public Property UserId As Integer
    <DataMember()>
    Public Property LastLoadTime As DateTime
End Class
