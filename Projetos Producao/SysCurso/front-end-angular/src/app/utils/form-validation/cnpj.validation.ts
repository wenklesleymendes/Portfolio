import { FormControl } from "@angular/forms";

/** Validação customizada de formulário */
export const validarCNPJ = (control: FormControl): {[key: string]: boolean} => {
  // Obtém dados
  const cnpj: Date = control.value;
  if(!cnpj) return;

  // return
  const valido: boolean = ValidaCamposCNPJ(cnpj);
  // Verifica se o cpf é inválido
  if(!valido) return { 'cnpjInvalido': true }; // Se for inválido irá setar um erro
}

export const ValidaCamposCNPJ = (cnpj): boolean => {
  cnpj = cnpj.replace(/[^\d]+/g,'');  
  if (cnpj.length != 14) return false;
  if (cnpj == "00000000000000") return false;
        
  let tamanho = cnpj.length - 2
  let numeros = cnpj.substring(0,tamanho);
  let digitos = cnpj.substring(tamanho);
  let soma = 0;
  let pos = tamanho - 7;

  for (let i = tamanho; i >= 1; i--) {
    soma += numeros.charAt(tamanho - i) * pos--;
    if (pos < 2) pos = 9;
  }

  let resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
  if (resultado != digitos.charAt(0)) return false;
        
  tamanho = tamanho + 1;
  numeros = cnpj.substring(0,tamanho);
  soma = 0;
  pos = tamanho - 7;

  for (let i = tamanho; i >= 1; i--) {
    soma += numeros.charAt(tamanho - i) * pos--;
    if (pos < 2) pos = 9;
  }

  resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
  if (resultado != digitos.charAt(1)) return false;          
  return true;
}