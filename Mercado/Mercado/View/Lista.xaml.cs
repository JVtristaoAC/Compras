using Mercado.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mercado.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

        public Lista()
        {
            InitializeComponent();

            list_Produto.ItemsSource = lista_produtos;
        }

        private void Cadastrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new Cadastro());
            }catch(Exception ex)
            {
                DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void Somar_Clicked(object sender, EventArgs e)
        {
            double soma = lista_produtos.Sum(i => i.preco * i.quantidade);

            string msg = "O total da Compra é: " + soma;

            DisplayAlert("Soma", msg, "OK");
        }

        //Método para criar uma tarefa em segundo plano (Thread)
        protected override void OnAppearing()
        {
            if(lista_produtos.Count == 0) {

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Produto> temp = await App.BD.select();

                foreach (Produto item in temp)
                {
                    lista_produtos.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
            }
        }

        private async void Remover_Clicked(object sender, EventArgs e)
        {
            MenuItem disparador = (MenuItem)sender;
            Produto produto_selecionado = (Produto)disparador.BindingContext;

            bool Confirmacao = await DisplayAlert("Tem Certeza?", "Remover Item?", "Sim", "Não");
            if (Confirmacao)
            {
                await App.BD.Delete(produto_selecionado.id);
                lista_produtos.Remove(produto_selecionado);
            }
        }

        private void txt_Busca_TextChanged(object sender, TextChangedEventArgs e)
        {
            string buscou = e.NewTextValue;

            System.Threading.Tasks.Task.Run(async () =>
            {
                List<Produto> temp = await App.BD.Search(buscou);

                lista_produtos.Clear();

                foreach (Produto item in temp)
                {
                    lista_produtos.Add(item);
                }

                ref_carregando.IsRefreshing = false;
            });
        }

        private void list_Produto_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Navigation.PushAsync(new Editar
            {
                BindingContext = (Produto)e.SelectedItem
            });
        }
    }
}