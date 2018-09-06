using System;
using System.Windows.Forms;

namespace AutodeskForgeObjectPrepAndViewGenerator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var view = new ToolView();
            view.Text += @" v" + Application.ProductVersion;

            var model = new ForgeClient("IZUjerfx4lHNscEcgGDC9lPnniswRh3C", "fY0j7IeC0zYVXbAI");

            var presenter = new ToolPresenter(view, model);
            presenter.Run();

            Application.Run(view);
        }
    }
}
