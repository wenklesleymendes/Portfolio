var tbody = document.querySelector('table tbody');
var carro = {};

function Cadastrar() {	
	carro.Marca = document.querySelector('#Marca').value;
	carro.Cor = document.querySelector('#Cor').value;
	carro.Placa = document.querySelector('#Placa').value;

	console.log(carro);

	if (carro.id === undefined || carro.id === 0)
	{
		Salvar('POST', 0, carro);
	}			
	else
	{
		Salvar('PUT', carro.id, carro);
	}		

	Carregar();

	$('#myModal').modal('hide');
}

function Novo() {
	var btnSalvar = document.querySelector('#btnSalvar');	
	var tituloModal = document.querySelector('#tituloModal');
	document.querySelector('#Marca').value = '';
	document.querySelector('#Cor').value = '';
	document.querySelector('#Placa').value = '';

	carro = {};

	btnSalvar.textContent = 'Cadastrar';	

	tituloModal.textContent = 'Cadastrar novo carro';

	$('#myModal').modal('show');
}

function Cancelar() {
	var btnSalvar = document.querySelector('#btnSalvar');	
	var tituloModal = document.querySelector('#tituloModal');
	document.querySelector('#Marca').value = '';
	document.querySelector('#Cor').value = '';
	document.querySelector('#Placa').value = '';

	carro = {};

	btnSalvar.textContent = 'Cadastrar';	

	tituloModal.textContent = 'Cadastrar carro';

	$('#myModal').modal('hide');
}

function Carregar() {
	tbody.innerHTML = '';

	var xhr = new XMLHttpRequest();

	xhr.open(`GET`, `http://localhost:50221/api/carro/Recuperar`, true); 
	xhr.setRequestHeader('Authorization', sessionStorage.getItem('token'));   

	xhr.onerror = function () {
		console.error('ERRO', xhr.readyState);
	}

	xhr.onreadystatechange = function() {
		if (this.readyState == 4){
			if(this.status == 200) {
				var automovels = JSON.parse(this.responseText);
				for(var indice in automovels){
					AddLinha(automovels[indice]);
				}
			}
			else if(this.status == 500){
				var erro = JSON.parse(this.responseText);
				console.log(erro.message);
				console.log(erro.exceptionMessage);
			}
		}
	}

	xhr.send();	
}

function Salvar(metodo, id, corpo) {
	var xhr = new XMLHttpRequest();

	if (id === undefined || id === 0)
		id = '';

	xhr.open(metodo, `http://localhost:50221/api/carro/${id}`, false);

	xhr.setRequestHeader('content-type', 'application/json');
	xhr.send(JSON.stringify(corpo));
}

function Novo(id) {
	var xhr = new XMLHttpRequest();

	xhr.open(`DELETE`, `http://localhost:50221/api/carro/${id}`, false);

	xhr.send();
}

function excluir(automovel) {

	bootbox.confirm({
		message: `Tem certeza que deseja exluir o automovel ${automovel.Marca}`,
		buttons: {
			confirm: {
				label: 'SIM',
				className: 'btn-success'
			},
			cancel: {
				label: 'NÃO',
				className: 'btn-danger'
			}
		},
		callback: function (result) {
			if(result){	
				Novo(automovel.id);
				Carregar();
			}
		}
	});
}

Carregar();

function Editar(automovel){
	var btnSalvar = document.querySelector('#btnSalvar');	
	var tituloModal = document.querySelector('#tituloModal');
	document.querySelector('#Marca').value = automovel.Marca;
	document.querySelector('#Cor').value = automovel.Cor;
	document.querySelector('#Placa').value = automovel.Placa;

	btnSalvar.textContent = 'Salvar';

	tituloModal.textContent = `Editar carro ${automovel.Marca}`;

	carro = automovel;

	console.log(carro);
}

function AddLinha(carro) {
	var trow = `<tr>
	<td>${automovel.Marca}</td>
	<td>${automovel.Cor}</td>
	<td>${automovel.Placa}</td>
	<td>
	<div class="btn-group" role="group">
	<button class="btn btn-info" data-toggle="modal" data-target="#myModal" onclick='Editar(${JSON.stringify(automovel)})'>Editar</button>
	<button class="btn btn-danger" onclick='excluir(${JSON.stringify(automovel)})'>Excluir</button>
	</div>
	</td>
	</tr>`;

	tbody.innerHTML += trow;
}