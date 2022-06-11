using Mercado.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cadastro : ContentPage
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private async void Salvar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Produto p = new Produto
                {
                    descricao = txt_descricao.Text,
                    quantidade = Convert.ToDouble(txt_quantidade.Text),
                    preco = Convert.ToDouble(txt_preco.Text)

                };

               await App.BD.Insert(p);
               await DisplayAlert("Cadastro concluído", "Produto cadastrado com sucesso", "OK");

               
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}