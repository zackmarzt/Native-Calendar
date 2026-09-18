# Native Calendar

Um aplicativo de calendário nativo simples para Windows, desenvolvido em C# utilizando o framework WPF (Windows Presentation Foundation) e .NET 10.0.

## 📌 Sobre o Projeto

O **Native Calendar** permite visualizar um calendário mensal, selecionar dias específicos e gerenciar eventos (adicionar e remover). Todos os eventos criados são armazenados localmente na máquina, garantindo que as informações persistam mesmo após o fechamento do aplicativo.

O projeto segue o padrão arquitetural **MVVM** (Model-View-ViewModel), o que mantém uma separação limpa entre a interface do usuário (UI) e a lógica de negócios.

## 🚀 Funcionalidades

- **Navegação Mensal**: Avance ou retroceda os meses, ou retorne ao mês atual rapidamente.
- **Seleção de Dias**: Clique em um dia para ver os eventos associados a ele.
- **Gerenciamento de Eventos**: Adicione novos eventos a uma data selecionada ou remova eventos existentes.
- **Armazenamento Local**: Seus eventos são salvos e carregados automaticamente utilizando um sistema de armazenamento de arquivos (`EventStore.cs`).
- **Interface Responsiva**: Feedback de status para informar quando os eventos são salvos ou removidos.

## 🛠️ Tecnologias e Bibliotecas Utilizadas

O projeto foi construído usando ferramentas e bibliotecas padrão do ecossistema .NET, não dependendo de pacotes NuGet externos de terceiros.

- **[.NET 10.0](https://dotnet.microsoft.com/)**: O SDK base utilizado no projeto.
- **WPF (Windows Presentation Foundation)**: Framework nativo da Microsoft para criação da interface gráfica para Windows.
- **C#**: Linguagem de programação.
- Bibliotecas do Sistema (System):
  - `System.Collections.ObjectModel`: Utilizado para `ObservableCollection`, que notifica a interface sobre adições/remoções na lista de eventos.
  - `System.ComponentModel`: Fornece a interface `INotifyPropertyChanged` para atualizar a UI quando as propriedades do ViewModel mudam.
  - `System.Windows.Input`: Para implementação de comandos (`ICommand`) como o `RelayCommand`, lidando com cliques de botões.
  - `System.IO`: Utilizado para manipulação de arquivos (leitura e gravação) no `EventStore.cs`.
  - `System.Text.Json` (provavelmente no `EventStore.cs`): Para serializar e desserializar os eventos localmente.

## 📂 Estrutura do Projeto (Padrão MVVM)

- **`MainWindow.xaml` / `MainWindow.xaml.cs`**: A visão principal (View) onde a interface do calendário é declarada.
- **`CalendarViewModel.cs`**: O ViewModel principal que gerencia o estado da interface, a lista de dias do calendário e as ações de adicionar/remover eventos.
- **`CalendarDay.cs` & `CalendarEvent.cs`**: Modelos (Models) que representam as propriedades de um dia e de um evento, respectivamente.
- **`RelayCommand.cs`**: Uma implementação de `ICommand` para fazer a ligação entre as ações dos botões na interface e os métodos no ViewModel.
- **`EventStore.cs`**: Classe responsável pela persistência de dados. Carrega e salva a lista de eventos no disco.

## ⚙️ Como Executar

Para rodar este projeto, você precisará do **.NET SDK 10.0** instalado.

1. Abra o terminal na pasta raiz do projeto (`c:\Users\zack\Documents\Native-Calendar`).
2. Execute o comando para compilar o projeto:
   ```bash
   dotnet build
   ```
3. Execute o comando para rodar o aplicativo:
   ```bash
   dotnet run
   ```

---
*Projeto criado para fins de organização pessoal e estudos de WPF/C#.*
