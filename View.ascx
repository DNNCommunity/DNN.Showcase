<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Dnn.Showcase.View" %>

<div ng-app="DNN_Showcase" ng-cloak>
    <div view></div>
</div>

<script>
    var user_id = <%= UserId %>;
    var module_id = <%= ModuleId %>;
    var portal_id = <%= PortalId %>;
    var sf = $.ServicesFramework(module_id);
    var apiUrlBase = "<%= ApiUrlBase %>";
</script>
