import { RedeSocial } from './RedeSocial';
import { Lote } from './Lote';
import { PalestranteEvento } from './PalestranteEvento';

export interface Evento {
    id: number;
    local: string;
    dataEvento: Date;
    tema: string;
    qtdPessoas: number;
    imagemUrl: string;
    telefone: string;
    email: string;
    lotes: Lote[];
    redesSociais: RedeSocial[];
    palestranteEventos: PalestranteEvento[];
}
