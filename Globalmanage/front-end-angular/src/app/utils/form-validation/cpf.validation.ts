import { FormControl } from "@angular/forms";

/** Validação customizada de formulário */
export const validarCPF = (control: FormControl): {[key: string]: boolean} => {
  // Obtém dados
  const cpf: Date = control.value;
  if(!cpf) return;

  const valido: boolean = ValidaCamposCPF(cpf);
  // Verifica se o cpf é inválido
  if(!valido) return { 'cpfInvalido': true }; // Se for inválido irá setar um erro
}

export const ValidaCamposCPF = (strCPF): boolean => {
  let Soma: number = 0;
  let Resto: number = null;
  if (strCPF == "00000000000") return false;

  for (let i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (11 - i);
  Resto = (Soma * 10) % 11;

  if ((Resto == 10) || (Resto == 11))  Resto = 0;
  if (Resto != parseInt(strCPF.substring(9, 10)) ) return false;

  Soma = 0;
  for (let i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (12 - i);
  Resto = (Soma * 10) % 11;

  if ((Resto == 10) || (Resto == 11))  Resto = 0;
  if (Resto != parseInt(strCPF.substring(10, 11) ) ) return false;
  return true;
}