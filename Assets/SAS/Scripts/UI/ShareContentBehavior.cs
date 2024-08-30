using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using System.IO;
using NativeShareNamespace;

public class ShareContentBehavior : MonoBehaviour
{
    #region Members

    [SerializeField]
    private string DebugPath;

    private NativeShare m_nativeShare;

    #endregion

    #region Methods

    // Start is called before the first frame update
    void Start()
    {
        m_nativeShare = new NativeShare();
        m_nativeShare.Clear();

        ConvertInputsToPDF.Instance.PdfBuildFinished += PDFConverter_PdfBuildFinished;
    }

    private void PDFConverter_PdfBuildFinished(object sender, PDFEventArgs e)
    { 
        ShareContent(e.path);
    }

    public void ShareContent(string filePath)
    {
        Debug.LogFormat("DEBUG... PDF path: {0}", filePath);
        //m_nativeShare.AddFile(filePath, null);
        //m_nativeShare.Share();
    }

    #endregion
}
