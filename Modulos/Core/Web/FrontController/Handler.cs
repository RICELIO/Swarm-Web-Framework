using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Swarm.Core.Web.FrontController;
using Swarm.Core.Web.FrontController.Common;

public class Handler : IHttpHandler, IRequiresSessionState
{
    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Items[PageFacade.HANDLER_PARAMETROS] = context.Request.QueryString.ToString();
        context.Items[PageFacade.HANDLER_PARAMETROS_ADICIONAIS] = context.Request.PathInfo;

        //try
        //{
        PageFacade objPAGE = PageFactory.Create(context);
        Page page = objPAGE.GetPageRequested();

        page.PreRender += new EventHandler(PreRenderCompleted);
        page.ProcessRequest(context);
        //}
        //catch (Exception erro) { Navigation.ShowMessage(erro.Message); }
    }

    #endregion

    #region Métodos Internos

    private void PreRenderCompleted(object sender, EventArgs e)
    {
        HttpContext.Current.RewritePath(HttpContext.Current.Request.Path, HttpContext.Current.Items[PageFacade.HANDLER_PARAMETROS_ADICIONAIS].ToString(), HttpContext.Current.Items[PageFacade.HANDLER_PARAMETROS].ToString());
    }

    #endregion
}