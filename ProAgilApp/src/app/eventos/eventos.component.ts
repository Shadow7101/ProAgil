import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { defineLocale, BsLocaleService, ptBrLocale } from 'ngx-bootstrap';
import { ToastrService } from 'ngx-toastr';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  titulo = 'Eventos';
  _filtroLista: string;
  eventosFiltrados: Evento[];
  eventos: Evento[];
  evento: Evento;
  imagemLargura = 50;
  imagemMargem = 2;
  mostrarImagem = false;
  registerForm: FormGroup;
  novoRegistro = false;
  file: File;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService) {
    this.localeService.use('pt-br');
  }

  get filtroLista(): string {
    return this._filtroLista;
  }
  set filtroLista(value: string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEvento(this.filtroLista) : this.eventos;
  }

  ngOnInit() {
    this.validation();
    this.getEventos();
  }

  filtrarEvento(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1);
  }


  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (eventos: Evento[]) => {
        eventos.forEach(evento => {
          console.log(evento.dataEvento);
        });
        this.eventos = eventos;
        this.eventosFiltrados = eventos;
      }, error => {
        console.log(error);
        this.toastr.error(`Erro ao carregar! ${error}`, 'Erro!');
      });
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  editarEvento(model: Evento, template: any): void {
    this.openModal(template);
    this.evento = model;
    this.registerForm.patchValue(model);
    this.novoRegistro = false;
  }

  openModal(template: any): void {
    this.registerForm.reset();
    template.show(template);
    this.novoRegistro = true;
  }

  validation(): void {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });

    /*
    this.registerForm = new FormGroup({
      tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      local: new FormControl('', [Validators.required]),
      dataEvento: new FormControl('', [Validators.required]),
      qtdPessoas: new FormControl('', [Validators.required, Validators.max(120000)]),
      imagemUrl: new FormControl('', [Validators.required]),
      telefone: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email])
    });*/
  }

  salvarAlteracao(template: any): void {
    if (!this.registerForm.valid) { return; }

    if (this.novoRegistro) {
      this.evento = Object.assign({}, this.registerForm.value);
      this.eventoService.postEvento(this.evento).subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          template.hide();
          this.getEventos();
          this.toastr.success('Salvo com sucesso!', 'Sucesso!');
        }, error => {
          console.error(error);
          this.toastr.error(`Falha ao salvar! ${error}`, 'Erro!');
        });
    }
    else {
      this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
      this.eventoService.putEvento(this.evento).subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          template.hide();
          this.getEventos();
          this.toastr.success('Salvo com sucesso!', 'Sucesso!');
        }, error => {
          console.error(error);
          this.toastr.error(`Falha ao salvar! ${error}`, 'Erro!');
        });
    }
  }

  confirmeDelete(evento: Evento, confirmeModal: any): void {
    this.evento = evento;
    this.openModal(confirmeModal);
  }

  excluirEvento(confirmeModal: any): void {
    this.eventoService.deleteEvento(this.evento.id).subscribe(
      () => {
        this.getEventos();
        confirmeModal.hide();
        this.toastr.success('Excluído com sucesso!', 'Sucesso!');
      }, error => {
        console.error(error);
        this.toastr.error(`Falha ao excluir! ${error}`, 'Erro!');
      });
  }


  OnFileChange(fileEvent: any): void {
    if (!fileEvent.target.files || fileEvent.target.files.length == 0) {
      return;
    }
    const reader = new FileReader();
    this.file = fileEvent.target.files;
    this.eventoService.Upload(fileEvent.target.files).subscribe((response: any) => {
      // this.toastr.success('Enviado com sucesso!', 'Sucesso!');
      console.log(response);
      this.SetImageUrlValue(response.fileName);
    }, error => {
      console.error(error);
      this.toastr.error(`Erro ao fazer upload! ${error.message}`, 'Erro!');
    });
  }

  SetImageUrlValue(value: string) {
    if (this.novoRegistro) {
      this.evento = Object.assign({}, this.registerForm.value);
    } else {
      this.evento = Object.assign({ id: this.evento.id }, this.registerForm.value);
    }
    this.evento.imagemUrl = value;
    this.registerForm.patchValue(this.evento);
  }
}
