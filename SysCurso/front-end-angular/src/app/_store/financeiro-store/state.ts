export interface Financeiro {
    planoPagamento?: PlanoPagamento,
    dadosSelecionados?: DadosSelecionados,
    campanha?: Campanha,
    financeiroCadastrado?: FinanceiroCadastrado
    comprovante: Comprovante
}

interface PlanoPagamento {
    [propName: string]: any;
}

interface Comprovante {
    [propName: string]: any;
}

interface DadosSelecionados {
    [propName: string]: any;
}

interface Campanha {
    [propName: string]: any;
}

interface FinanceiroCadastrado {
    matriculaId?: number,
    pagamento?: number,
    planoPagamentoAluno?: PlanoPagamentoAluno,
    [propName: string]: any;
}

interface PlanoPagamentoAluno {
    id?: number,
    tipoPagamento?: number,
    planoPagamentoId?: number,
    campanhaId?: number,
    [propName: string]: any;
}