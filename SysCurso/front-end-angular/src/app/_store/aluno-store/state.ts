export interface Aluno {
    cadastro?: CadastroAluno
    matricula?: Matricula
    img?: Img
    curso?: Curso
}

interface CadastroAluno {
    [propName: string]: any;
}

interface Matricula {
    [propName: string]: any;
}

interface Curso {
    [propName: string]: any;
}

interface Img {
    foto: string,
    extensao: string
}