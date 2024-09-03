<h1>gRPC Streaming Application</h1>

<p>Este projeto é uma aplicação de streaming bidirecional usando gRPC em C#. A aplicação consiste em um cliente e um servidor que se comunicam através de streams gRPC para duas funcionalidades principais:</p>

<ul>
    <li>Envio de palavras e recebimento da palavra de maior comprimento até o momento.</li>
    <li>Verificação se uma pessoa é maior de idade (18 anos ou mais).</li>
</ul>

<h2>Requisitos</h2>

<ul>
    <li>.NET SDK 5.0 ou superior</li>
    <li>Visual Studio ou qualquer outra IDE compatível com C#</li>
    <li>gRPC.Tools para gerar as classes C# a partir do arquivo .proto</li>
</ul>

<h2>Como Rodar o Projeto</h2>

<h3>1. Clonar o Repositório</h3>

<pre>
git clone &lt;https://github.com/izaltinoromao/DM113-grpc.git&gt;
cd grpc_streaming
</pre>

<h3>2. Compilar as Classes gRPC</h3>

<p>Navegue até a pasta <code>protos</code> e execute o comando para compilar as definições <code>.proto</code>:</p>

<pre>
dotnet build
</pre>

<h3>3. Rodar o Servidor</h3>

<p>Navegue até a pasta do servidor e execute:</p>

<pre>
cd server
dotnet run
</pre>

<p>O servidor iniciará e estará escutando na porta <code>50005</code>.</p>

<h3>4. Rodar o Cliente</h3>

<p>Em outra janela de terminal, navegue até a pasta do cliente e execute:</p>

<pre>
cd client
dotnet run
</pre>

<p>O cliente começará a enviar mensagens para o servidor e a receber as respostas conforme as regras de cada serviço.</p>

<h2>Visão Geral do gRPC no Projeto</h2>

<h3>Definições gRPC</h3>

<p>O projeto utiliza gRPC para comunicação bidirecional entre o cliente e o servidor. A definição das mensagens e serviços está no arquivo <code>messages.proto</code>.</p>

<p><strong>Mensagens Definidas:</strong></p>
<ul>
    <li><code>SingleWordMessage</code>: Contém uma palavra.</li>
    <li><code>SinglePersonMessage</code>: Contém um nome e uma idade.</li>
    <li><code>SingleOverAgeMessage</code>: Contém uma mensagem indicando se a pessoa é maior de idade.</li>
</ul>

<p><strong>Serviços Definidos:</strong></p>
<ul>
    <li><code>CheckIfOverAge</code>: Recebe um stream de <code>SinglePersonMessage</code> e retorna um stream de <code>SingleOverAgeMessage</code> para pessoas com 18 anos ou mais.</li>
    <li><code>GetMostLengthString</code>: Recebe um stream de <code>SingleWordMessage</code> e retorna um stream com a palavra de maior comprimento até o momento.</li>
</ul>

<h3>Funcionamento</h3>

<ol>
    <li><strong>Servidor (<code>server/Program.cs</code>):</strong> Inicializa e expõe os serviços definidos no arquivo <code>.proto</code>.</li>
    <li><strong>Serviços (<code>server/services/MessageServicesImpl.cs</code>):</strong> Implementa a lógica para os métodos de serviço, incluindo:
        <ul>
            <li>Verificação de idade.</li>
            <li>Determinação da palavra de maior comprimento.</li>
        </ul>
    </li>
    <li><strong>Cliente (<code>client/Program.cs</code>):</strong> Envia dados para o servidor e processa as respostas recebidas.</li>
</ol>

<h2>APIs e Endpoints</h2>

<h3><code>CheckIfOverAge</code></h3>

<ul>
    <li><strong>Descrição:</strong> Recebe um stream de mensagens com o nome e a idade de uma pessoa e verifica se ela é maior de idade.</li>
    <li><strong>Endpoint:</strong> <code>/CheckIfOverAge</code></li>
    <li><strong>Request:</strong> <code>SinglePersonMessage</code> (stream)</li>
    <li><strong>Response:</strong> <code>SingleOverAgeMessage</code> (stream)</li>
    <li><strong>Exemplo de Request:</strong> 
        <pre>
{
    "name": "Lucas",
    "idade": 22
}
        </pre>
    </li>
    <li><strong>Exemplo de Response:</strong> 
        <pre>
{
    "message": "Lucas é maior de idade"
}
        </pre>
    </li>
</ul>

<h3><code>GetMostLengthString</code></h3>

<ul>
    <li><strong>Descrição:</strong> Recebe um stream de palavras e retorna o maior comprimento de palavra até o momento.</li>
    <li><strong>Endpoint:</strong> <code>/GetMostLengthString</code></li>
    <li><strong>Request:</strong> <code>SingleWordMessage</code> (stream)</li>
    <li><strong>Response:</strong> <code>SingleWordMessage</code> (stream)</li>
    <li><strong>Exemplo de Request:</strong> 
        <pre>
{
    "word": "jubileudeazeu"
}
        </pre>
    </li>
    <li><strong>Exemplo de Response:</strong> 
        <pre>
{
    "word": "jubileudeazeu"
}
        </pre>
    </li>
</ul>

<h2>Colaboradores</h2>

## 👥 Authors

<table>
  <tr>
    <td>
      <h4 align="center">
        <img style="border-radius: 50%" src="https://avatars.githubusercontent.com/u/49520648?v=4" width="180px;" alt="Izaltino Romão Neto">
      </h4>
      <strong>Izaltino Romão Neto</strong>
      <br>
      <a href="https://www.linkedin.com/in/izaltino-rom%C3%A3o-neto-0b60a8221/">
        <img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" alt="LinkedIn Badge">
      </a>
      <a href="mailto:izaltino.neto@pg.inatel.br">
        <img src="https://img.shields.io/badge/Outlook-0078D4?style=for-the-badge&logo=microsoft-outlook&logoColor=white" alt="Outlook Badge">
      </a>
    </td>
<td>
      <h4 align="center">
        <img style="border-radius: 50%" src="https://avatars.githubusercontent.com/u/99922083?v=4" width="180px;" alt="Lucas de Souza Resende">
      </h4>
      <strong>Lucas de Souza Resende</strong>
      <br>
      <a href="https://www.linkedin.com/in/lucassresende/">
        <img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" alt="LinkedIn Badge">
      </a>
      <a href="mailto:l.souza@pg.inatel.br">
        <img src="https://img.shields.io/badge/Outlook-0078D4?style=for-the-badge&logo=microsoft-outlook&logoColor=white" alt="Outlook Badge">
      </a>
    </td>
