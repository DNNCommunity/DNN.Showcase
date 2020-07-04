<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" Inherits="Dnn.Showcase.CategoryList" %>

<div ng-app="DNN_Showcase" ng-cloak>
    <div category-list></div>
</div>

<script>
    var module_id = <%= ModuleId %>;
    var portal_id = <%= PortalId %>;
    var sf = $.ServicesFramework(module_id);
    var apiUrlBase = "<%= ApiUrlBase %>";
</script>
