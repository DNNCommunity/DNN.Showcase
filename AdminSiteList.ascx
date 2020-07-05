<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminSiteList.ascx.cs" Inherits="Dnn.Showcase.AdminSiteList" %>

<div ng-app="DNN_Showcase" ng-cloak>
    <div site-list></div>
</div>

<script>
    var module_id = <%= ModuleId %>;
    var portal_id = <%= PortalId %>;
    var sf = $.ServicesFramework(module_id);
    var apiUrlBase = "<%= ApiUrlBase %>";
    var navigateUrl = "<%= DotNetNuke.Common.Globals.NavigateURL(TabId) %>"
</script>
