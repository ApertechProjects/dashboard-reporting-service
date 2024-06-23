using DevExpress.AspNetCore.Reporting.QueryBuilder;
using DevExpress.AspNetCore.Reporting.QueryBuilder.Native.Services;
using DevExpress.AspNetCore.Reporting.ReportDesigner;
using DevExpress.AspNetCore.Reporting.ReportDesigner.Native.Services;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer;
using DevExpress.AspNetCore.Reporting.WebDocumentViewer.Native.Services;
using DevExpress.XtraReports.Web.ReportDesigner.Services;
using Microsoft.AspNetCore.Mvc;

namespace ReportingService.Controllers;

public class CustomReportDesignerController : ReportDesignerController {
    
    public CustomReportDesignerController(IReportDesignerMvcControllerService controllerService) : base(controllerService) {
    }
    
    [HttpPost("[action]")]
    public async Task<IActionResult> GetDesignerModel(
        [FromForm] string reportName,
        [FromServices] IReportDesignerModelBuilder reportDesignerModelBuilder) {
        var designerModel = await reportDesignerModelBuilder
            .Report(reportName)
            .BuildModelAsync();

        return DesignerModel(designerModel);
    }
}

public class CustomQueryBuilderController : QueryBuilderController {
    public CustomQueryBuilderController(IQueryBuilderMvcControllerService controllerService) : base(controllerService) {
    }
}

public class CustomWebDocumentViewerController : WebDocumentViewerController {
    public CustomWebDocumentViewerController(IWebDocumentViewerMvcControllerService controllerService) : base(controllerService) {
    }
}