import { Component, OnInit } from '@angular/core';
import { EventoService } from 'src/app/_services/evento.service';
import { BsModalService, BsLocaleService } from 'ngx-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-edit',
  templateUrl: './evento-edit.component.html',
  styleUrls: ['./evento-edit.component.css']
})
export class EventoEditComponent implements OnInit {
  titulo = 'Editar evento';
  registerForm: FormGroup;
  evento = {};
  imagemURL = 'assets/img/upload.png';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private toastr: ToastrService) {
    this.localeService.use('pt-br');
  }


  ngOnInit() {
    this.validation();
  }

  validation(): void {
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL: [''],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      lotes: this.fb.group({
        nome: ['', Validators.required],
        quantidade: ['', Validators.required],
        preco: ['', Validators.required],
        dataInicio: [''],
        dataFim: ['']
      }),
      redesSociais: this.fb.group({
        nome: ['', Validators.required],
        url: ['', Validators.required]
      })
    });

  }



  onFileChange(files: FileList): void {
    const reader = new FileReader();
    reader.onload = (event: any) => this.imagemURL = event.target.result;
    reader.readAsDataURL(files[0]);
  }
}
