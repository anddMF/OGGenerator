# OGGenerator
O <b>Original Gangsta Generator</b> tem a função de gerar arquivos .cs com as models de sua tabela ou procedure do DB. Exemplo:
<br>
```
        public int ID { get; set; }
        public string NAME { get; set; }
        public DateTime DT_REGISTER { get; set; }
```
# Funcionamento
Através do App.config o usuário coloca a sua connection string (por enquanto apenas em SQL Server) e o caminho da pasta que deseja os arquivos.
Após, rodará a aplicação seguindo o passo a passo que será mostrado na tela, podendo optar por quatro opções:
<ul>
  <li>Gerar models de todas as tabelas do banco;</li>
  <li>Gerar models de uma tabela em específico;</li>
  <li>Gerar models de uma procedure.</li>
</ul>

# Novas atualizações
- [ ] Adaptação para Windows Forms com o intuito de facilitar o uso.
- [ ] Funcionalidade também para bancos DB2.
<br>
<br>
<h4>Contato: <a href="https://www.linkedin.com/in/andrew-moraes-93861b142/">Andrew Moraes</a></h4>
