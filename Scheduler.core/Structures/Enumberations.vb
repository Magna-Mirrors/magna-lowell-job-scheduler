Imports System
Imports System.Runtime.Serialization

<DataContract(Name:="SchedulerMethods")>
Public Enum SchedulerMethods
    <EnumMember> [None] = 0
    <EnumMember> MdbAndPlanFiles = 1
    <EnumMember> MsSql = 2
End Enum
<DataContract(Name:="PartStatus")>
Public Enum PartStatus
	<EnumMember> NF = 0
	<EnumMember> OK = 1
	<EnumMember> DT = 2
	<EnumMember> ND = 3
End Enum

Public Enum Part_Definition_Status
	Unavailable = 0
	NewlyInserted = 1
	AvailableForProduction = 2
	NewlyUpdated = 3
End Enum

<DataContract(Name:="PlanStatus")>
Public Enum PlanStatus
    <EnumMember> Unknown = 0
    <EnumMember> Suspended = 1
    <EnumMember> Planed = 2
    <EnumMember> Scheduled = 3
    <EnumMember> Complete = 4
    <EnumMember> Removed = 5
End Enum



<DataContract(Name:="LogType")>
Public Enum LogType
    <EnumMember> Warn
    <EnumMember> [Error]
    <EnumMember> info
    <EnumMember> Success
    <EnumMember> plcevent
End Enum



<DataContract(Name:="NotifyOptions")>
Public Enum NotifyOptions
    <EnumMember> [None] = 0
    <EnumMember> Display = 1
    <EnumMember> Log = 2
    <EnumMember> Email = 4
End Enum

<DataContract(Name:="uAccessLevels")>
Public Enum uAccessLevels
    <EnumMember> [None] = 0
    <EnumMember> [Operator] = 1
    <EnumMember> Supervisor = 2
    <EnumMember> Tech = 3
    <EnumMember> Engineer = 4
    <EnumMember> Admin = 5
End Enum

<DataContract(Name:="OrderFlags"), Flags>
Public Enum OrderFlags
    <EnumMember> Truck = 1
    <EnumMember> C = 2
    <EnumMember> Flag2 = 4
    <EnumMember> Flag3 = 8
    <EnumMember> RequiresCustomerOrderId = 16
End Enum