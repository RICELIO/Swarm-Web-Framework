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
    #region Constantes

    private const string PARAMETROS = "Handler_Orig_Params";
    private const string PARAMETROS_ADICIONAIS = "Handler_Orig_AdditionalParams";

    #endregion

    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Items[PARAMETROS] = context.Request.QueryString.ToString();
        context.Items[PARAMETROS_ADICIONAIS] = context.Request.PathInfo;

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
        HttpContext.Current.RewritePath(HttpContext.Current.Request.Path, HttpContext.Current.Items[PARAMETROS_ADICIONAIS].ToString(), HttpContext.Current.Items[PARAMETROS].ToString());
    }

    #endregion
}