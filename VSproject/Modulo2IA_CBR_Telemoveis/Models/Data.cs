using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modulo2IA_CBR_Telemoveis.Models
{
    public class Data
    {
        public DataClassesCBRDataContext DataBase { get; set; }
        public Telemovel tm { get; set; }
        public Camera c { get; set; }
        public Ecra e { get; set; }
        public Processador p { get; set; }

        public Data()
        {
            tm = new Telemovel();
            e = new Ecra();
            c = new Camera();
            p = new Processador();
            DataBase = new DataClassesCBRDataContext();
        }

        public void SetTelemovel(int _idMarca, int _idResolução, int _idEstado, double _ram, int _memoriaInterna, int _mAhBateria,
            double _tamanhoEcra, double _velocidadeProcessador, int _nucleosProcessador, double _resolucaoCameraFrontal,
            double _resolucaoCameraTraseira, int _idade)
        {
            tm.idMarca = _idMarca;  //id para ligação à classe já existente
            e.idResoluçãoEcra = _idResolução;  //id para ligação à classe já existente
            tm.idEstado = _idEstado;    //id para ligação à classe já existente
            tm.ram = _ram;              //quantidade de memoria ram (é dada em quantidades que podem ser floats)
            tm.memoriaInterna = _memoriaInterna; //quantidade de memoria interna
            tm.mAhBateria = _mAhBateria; //mAh da bateria
            e.tamanho = _tamanhoEcra; //em polegadas
            p.velocidadeProcessador = _velocidadeProcessador;
            p.nucleosProcessador = _nucleosProcessador;  //numero de nucleos do processador
            c.resolucaoFrontal = _resolucaoCameraFrontal; //resolução da camera frontal
            c.resolucaoTraseira = _resolucaoCameraTraseira; //resolução da camera traseira
            tm.idade = _idade; //dada em meses
        }

        public void AdicionarCaso()
        {
            submeterNaDB();
            tm = new Telemovel();
            e = new Ecra();
            c = new Camera();
            p = new Processador();
        }

        public void EditarCaso(int id, int _idMarca, int _idResolução, int _idEstado, double _ram, int _memoriaInterna, int _mAhBateria,
            double _tamanhoEcra, double _velocidadeProcessador, int _nucleosProcessador, double _resolucaoCameraFrontal,
            double _resolucaoCameraTraseira, int _idade, int _valor)
        {
            DataBase.Telemovels.Single(x => x.idTelemovel == id).idMarca = _idMarca;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).Ecra.idResoluçãoEcra = _idResolução;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).idEstado = _idEstado;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).ram = _ram;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).memoriaInterna = _memoriaInterna;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).mAhBateria = _mAhBateria;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).Ecra.tamanho = _tamanhoEcra;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).Processador.velocidadeProcessador = _velocidadeProcessador;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).Processador.nucleosProcessador = _nucleosProcessador;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).Camera.resolucaoFrontal = _resolucaoCameraFrontal;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).Camera.resolucaoTraseira = _resolucaoCameraTraseira;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).idade = _idade;
            DataBase.Telemovels.Single(x => x.idTelemovel == id).valorFinal = _valor;
            DataBase.SubmitChanges();
        }
        public void submeterNaDB()
        {
            DataBase.Cameras.InsertOnSubmit(c);
            DataBase.Ecras.InsertOnSubmit(e);
            DataBase.Processadors.InsertOnSubmit(p);
            DataBase.SubmitChanges();

            tm.idCameras = c.idCameras;
            tm.idEcra = e.idEcra;
            tm.idProcessador = p.idProcessador;
            DataBase.Telemovels.InsertOnSubmit(tm);
            DataBase.SubmitChanges();
        }

        public double SimiliridadeMarca(string marca1, string marca2)
        {
            if (marca1 == marca2) return 1;
            else if (marca1 == "Apple" && marca2 == "Samsung") return 0.8; //similiridade em termos de preço
            else if (marca1 == "Samsung" && marca2 == "Apple") return 0.8;
            else if (marca1 == "LG" && marca2 == "Samsung") return 0.5;
            else if (marca1 == "Samsung" && marca2 == "LG") return 0.5;
            else if (marca1 == "LG" && marca2 == "Apple") return 0.4;
            else if (marca1 == "Apple" && marca2 == "LG") return 0.4;
            else if (marca1 == "Outras" && marca2 == "LG") return 0.5;
            else if (marca1 == "LG" && marca2 == "Outras") return 0.5;
            else if (marca1 == "Outras" && marca2 == "Samsung") return 0.3;
            else if (marca1 == "Samsung" && marca2 == "Outras") return 0.3;
            else if (marca1 == "Outras" && marca2 == "Apple") return 0.1;
            else if (marca1 == "Apple" && marca2 == "Outras") return 0.1;
            else return 0; //nunca poderá acontecer ser 0 porque a view não permite outras opções além das de cima.
        }

        public double SimiliridadeRam(double ram1, double ram2)
        {
            if (ram1 >= 10 && ram2 >= 10) return 1; //acima de 10 a ram é totalmente indiferente para um telemovel
            if (Math.Abs(ram1 - ram2) > 10) return 0; //nao teem semelhança nenhuma
            else
            {
                if (ram1 <= 2 && ram2 <= 2) //objetivo: acentuar a diferença para ram's com valores abaixo de 2
                    return (4 - Math.Abs(ram1 - ram2)) / 4; //varia de 0.5 a 1
                //exemplo: uma diferença de 1gb para 2gb é mais substancial do que 8gb para 9gb
                else
                    return (10 - Math.Abs(ram1 - ram2)) / 10;
            }
        }

        public double SimiliridadeMemoriaInterna(int mInterna1, int mInterna2)
        {
            if (mInterna1 >= 256 && mInterna2 >= 256) return 1; //acima de 256 a memoria nao tem importancia, sao iguais em termos de preço
            if (Math.Abs(mInterna1 - mInterna2) > 256) return 0; //nao teem semelhança nenhuma
            else
            {
                if (mInterna1 <= 5 && mInterna2 <= 5) //objetivo: acentuar a diferença para memorias baixas
                    return (10 - Math.Abs(mInterna1 - mInterna2)) / 10; //varia de 0.5 a 1
                //exemplo: uma diferença de 5gb para 15gb é mais substancial do que 50gb para 60gb
                else
                    return (256 - Math.Abs(mInterna1 - mInterna2)) / 256;
            }
        }

        public double SimiliridademAhBateria(int mAhBateria1, int mAhBateria2)
        {
            if (mAhBateria1 >= 5000 && mAhBateria2 >= 5000) return 1; //não existem baterias com mais mAh
            if (Math.Abs(mAhBateria1 - mAhBateria2) > 5000) return 0;
            else
            {
                if (mAhBateria1 <= 1500 && mAhBateria2 <= 1500)
                    return (3000 - Math.Abs(mAhBateria1 - mAhBateria2)) / 3000; //varia de 0.5 a 1
                else
                    return (5000 - Math.Abs(mAhBateria1 - mAhBateria2)) / 5000;
            }
        }

        public double SimiliridadeResoluçãoEcra(string res1, string res2)
        {
            //"640x480 (VGA)"
            //"1280x720 (HD)"
            //"1920x1080 (FULL HD)"
            //"2560x1440 (2K)"
            //"3840x2160 (4K)"
            if (res1 == res2) return 1;

            if (res1 == "640x480 (VGA)" && res2 == "1280x720 (HD)") return 0.5;
            if (res1 == "1280x720 (HD)" && res2 == "640x480 (VGA)") return 0.5;

            if (res1 == "640x480 (VGA)" && res2 == "1920x1080 (FULL HD)") return 0.4;
            if (res1 == "1920x1080 (FULL HD)" && res2 == "640x480 (VGA)") return 0.4;

            if (res1 == "640x480 (VGA)" && res2 == "2560x1440 (2K)") return 0.2;
            if (res1 == "2560x1440 (2K)" && res2 == "640x480 (VGA)") return 0.2;

            if (res1 == "640x480 (VGA)" && res2 == "3840x2160 (4K)") return 0;
            if (res1 == "3840x2160 (4K)" && res2 == "640x480 (VGA)") return 0;
            //--------------
            if (res1 == "1920x1080 (FULL HD)" && res2 == "1280x720 (HD)") return 0.7;
            if (res1 == "1280x720 (HD)" && res2 == "1920x1080 (FULL HD)") return 0.7;

            if (res1 == "2560x1440 (2K)" && res2 == "1280x720 (HD)") return 0.5;
            if (res1 == "1280x720 (HD)" && res2 == "2560x1440 (2K)") return 0.5;

            if (res1 == "1280x720 (HD)" && res2 == "3840x2160 (4K)") return 0.2;
            if (res1 == "3840x2160 (4K)" && res2 == "1280x720 (HD)") return 0.2;
            //----------------
            if (res1 == "2560x1440 (2K)" && res2 == "1920x1080 (FULL HD)") return 0.6;
            if (res1 == "1980x1080 (FULL HD)" && res2 == "2560x1440 (2K)") return 0.6;

            if (res1 == "3840x2160 (4K)" && res2 == "1920x1080 (FULL HD)") return 0.4;
            if (res1 == "1980x1080 (FULL HD)" && res2 == "3840x2160 (4K)") return 0.4;
            //-----------------
            if (res1 == "3840x2160 (4K)" && res2 == "2560x1440 (2K)") return 0.5;
            if (res1 == "2560x1440 (2K)" && res2 == "3840x2160 (4K)") return 0.5;
            else return 0;

        }


        public double SimiliridadeTamanhoEcra(double tamanho1, double tamanho2)
        {//tamanho ideal de um ecra para telemovel segundo a maioria das sondagens: 5 polegadas
            if (tamanho1 > 10 && tamanho2 > 10) return 1; 
            if (Math.Abs(tamanho1 - tamanho2) < 5) return 5 - (Math.Abs(tamanho1 - tamanho2)) / 5;
            else
            {
                if (Math.Abs(tamanho1 - tamanho2) == 5) return 1;
                else
                    return (10 - Math.Abs(tamanho1 - tamanho2)) / 5;
            }
        }

        public double SimiliridadeVelocidadeProcessador(double vel1, double vel2)
        {//acima de 5GHz é indiferente
            if (vel1 >= 5 && vel2 >= 5) return 1;
            if (Math.Abs(vel1 - vel1) > 5) return 0;
            else
            {
                if (vel1 <= 1500 && vel2 <= 1.5)
                    return (3 - Math.Abs(vel1 - vel2)) / 3; //varia de 0.5 a 1
                else
                    return (5 - Math.Abs(vel1 - vel2)) / 5000;
            }
        }

        public double SimiliridadeNucleosProcessador(int n1, int n2)
        {//é inutil ter mais que 16 nucleos
            if (n1 >= 16 && n2 >= 16) return 1;
            if (Math.Abs(n1 - n2) > 16) return 0;
            else
            {
                if (n1 <= 2 && n2 <= 2)
                    return (4 - Math.Abs(n1 - n2)) / 4; //varia de 0.5 a 1
                else
                    return (16 - Math.Abs(n1 - n2)) / 16;
            }
        }

        public double SimiliridadeResolucaoCameraFrontal(double cFrontal1, double cFrontal2)
        {//no mercado nao existem cameras frontais com mais de 16 megapixels
            if (cFrontal1 >= 16 && cFrontal2 >= 16) return 1;
            if (Math.Abs(cFrontal1 - cFrontal2) > 16) return 0;
            else
            {
                if (cFrontal1 <= 4 && cFrontal2 <= 4)
                    return (8 - Math.Abs(cFrontal1 - cFrontal2)) / 8; //varia de 0.5 a 1
                else
                    return (16 - Math.Abs(cFrontal1 - cFrontal2)) / 16;
            }
        }

        public double SimiliridadeResolucaoCameraTraseira(double cTraseira1, double cTraseira2)
        {
            if (cTraseira1 >= 20 && cTraseira2 >= 20) return 1;
            if (Math.Abs(cTraseira1 - cTraseira2) > 20) return 0;
            else
            {
                if (cTraseira1 <= 8 && cTraseira2 <= 8)
                    return (16 - Math.Abs(cTraseira1 - cTraseira2)) / 16; //varia de 0.5 a 1
                else
                    return (20 - Math.Abs(cTraseira1 - cTraseira2)) / 20;
            }
        }

        public double SimiliridadeEstado(string estado1, string estado2)
        {
            int e1 = 0, e2 = 0;
            if (estado1 == "Péssimo") e1 = 0;
            if (estado1 == "Mau") e1 = 1;
            if (estado1 == "Intermédio") e1 = 2;
            if (estado1 == "Bom") e1 = 3;
            if (estado1 == "Muito Bom") e1 = 4;
            if (estado1 == "Como Novo") e1 = 5;

            if (estado2 == "Péssimo") e2 = 0;
            if (estado2 == "Mau") e2 = 1;
            if (estado2 == "Intermédio") e2 = 2;
            if (estado2 == "Bom") e2 = 3;
            if (estado2 == "Muito Bom") e2 = 4;
            if (estado2 == "Como Novo") e2 = 5;

            return (5 - Math.Abs(e1 - e2)) / 5;
        }

        public double SimiliridadeIdade(int idade1, int idade2)
        {//dada em meses
            if (idade1 >= 48 && idade2 >= 48) return 1; //acima de 4 anos é igual
            if (Math.Abs(idade1 - idade2) > 48) return 0;
            else
            {
                if (idade1 <= 6 && idade2 <= 6) //a diferença é mais relevante
                    return (12 - Math.Abs(idade1 - idade2)) / 12; //varia de 0.5 a 1
                else
                    return (48 - Math.Abs(idade1 - idade2)) / 48;
            }
        }

        public void Avaliação()
        {
            double melhorSimiliridade = 0; //ainda não foi encontrado nenhuma aproximação
            double similiridadeItem = 0;
            int idItem = 0;

            const double pesoMarca = 0.35;
            const double pesoRam = 0.075;
            const double pesoMemoriainterna = 0.05;
            const double pesomAhBateria = 0.05;
            const double pesoResEcra = 0.1;
            const double pesoTamanho = 0.025;
            const double pesoProcessadorvelocidade = 0.1;
            const double pesoProcessadornucleos = 0.025;
            const double pesoCamerafrontal = 0.025;
            const double pesoCameratraseira = 0.05;
            const double pesoEstado = 0.1;
            const double pesoIdade = 0.05;

            foreach (Telemovel item in DataBase.Telemovels)
            {
                //pesos acima
                similiridadeItem = SimiliridadeMarca(DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome, item.marca.nome) * pesoMarca + SimiliridadeRam(tm.ram, item.ram) * pesoRam + SimiliridadeMemoriaInterna(tm.memoriaInterna, item.memoriaInterna) * pesoMemoriainterna
                                    + SimiliridademAhBateria(tm.mAhBateria, item.mAhBateria) * pesomAhBateria + SimiliridadeResoluçãoEcra(DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação, item.Ecra.ResoluçãoEcra.designação) * pesoResEcra
                                    + SimiliridadeTamanhoEcra(e.tamanho, item.Ecra.tamanho) * pesoTamanho + SimiliridadeVelocidadeProcessador(p.velocidadeProcessador, item.Processador.velocidadeProcessador) * pesoProcessadorvelocidade
                                    + SimiliridadeNucleosProcessador(p.nucleosProcessador, item.Processador.nucleosProcessador) * pesoProcessadornucleos + SimiliridadeResolucaoCameraFrontal(c.resolucaoFrontal, item.Camera.resolucaoFrontal) * pesoCamerafrontal
                                    + SimiliridadeResolucaoCameraTraseira(c.resolucaoTraseira, item.Camera.resolucaoTraseira) * pesoCameratraseira + SimiliridadeEstado(DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação, item.Estado.designação) * pesoEstado
                                    + SimiliridadeIdade(tm.idade, item.idade) * pesoIdade;

                if (similiridadeItem > melhorSimiliridade)
                {
                    melhorSimiliridade = similiridadeItem;
                    tm.valorFinal = Convert.ToInt32(item.valorFinal); //int da base de dados nao é compativel com int do visual studio
                    idItem = item.idTelemovel;
                }
            }
            submeterNaDB();

            if (similiridadeItem != 1)
                acertarPreço(DataBase.Telemovels.Single(x => x.idTelemovel == idItem));

            tm = new Telemovel(); //reinicializar para a proxima avaliação
            e = new Ecra();
            c = new Camera();
            p = new Processador();
        }
        public void acertarPreço(Telemovel caso)
        {
            List<Telemovel> listT = DataBase.Telemovels.OrderByDescending(p => p.idTelemovel).ToList();
            int idT = listT.First().idTelemovel;  //para buscarmos o ultimo a ser inserido (primeiro nesta lista porque está ordenada em descending)
            if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome != caso.marca.nome)
            {
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Apple" && caso.marca.nome == "Samsung") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.1);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Samsung" && caso.marca.nome == "Apple") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.1);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "LG" && caso.marca.nome == "Samsung") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.2);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Samsung" && caso.marca.nome == "LG") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.2);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "LG" && caso.marca.nome == "Apple") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.3);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Apple" && caso.marca.nome == "LG") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.3);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Outras" && caso.marca.nome == "LG") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.3);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "LG" && caso.marca.nome == "Outras") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.3);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Outras" && caso.marca.nome == "Samsung") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.4);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Samsung" && caso.marca.nome == "Outras") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.4);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Outras" && caso.marca.nome == "Apple") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.6);
                if (DataBase.marcas.Single(x => x.idMarca == tm.idMarca).nome == "Apple" && caso.marca.nome == "Outras") DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.6);
            }

            if (tm.ram > caso.ram)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeRam(tm.ram, caso.ram)) * 0.05);
            }

            if (tm.ram < caso.ram)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeRam(tm.ram, caso.ram)) * 0.05);
            }

            if (tm.memoriaInterna > caso.memoriaInterna)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeMemoriaInterna(tm.memoriaInterna, caso.memoriaInterna)) * 0.025);
            }

            if (tm.memoriaInterna < caso.memoriaInterna)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeMemoriaInterna(tm.memoriaInterna, caso.memoriaInterna)) * 0.025);
            }

            if (tm.mAhBateria > caso.mAhBateria)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridademAhBateria(tm.mAhBateria, caso.mAhBateria)) * 0.05);
            }

            if (tm.mAhBateria < caso.mAhBateria)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridademAhBateria(tm.mAhBateria, caso.mAhBateria)) * 0.05);
            }

            if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação != caso.Ecra.ResoluçãoEcra.designação)
            {
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "640x480 (VGA)" && caso.Ecra.ResoluçãoEcra.designação == "1280x720 (HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.05);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1280x720 (HD)" && caso.Ecra.ResoluçãoEcra.designação == "640x480 (VGA)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.05);

                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "640x480 (VGA)" && caso.Ecra.ResoluçãoEcra.designação == "1920x1080 (FULL HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.08);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1920x1080 (FULL HD)" && caso.Ecra.ResoluçãoEcra.designação == "640x480 (VGA)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.08);

                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "640x480 (VGA)" && caso.Ecra.ResoluçãoEcra.designação == "2560x1440 (2K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.1);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "2560x1440 (2K)" && caso.Ecra.ResoluçãoEcra.designação == "640x480 (VGA)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.1);

                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "640x480 (VGA)" && caso.Ecra.ResoluçãoEcra.designação == "3840x2160 (4K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.15);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "3840x2160 (4K)" && caso.Ecra.ResoluçãoEcra.designação == "640x480 (VGA)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.15);
                //--------------
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1920x1080 (FULL HD)" && caso.Ecra.ResoluçãoEcra.designação == "1280x720 (HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.06);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1280x720 (HD)" && caso.Ecra.ResoluçãoEcra.designação == "1920x1080 (FULL HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.06);

                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "2560x1440 (2K)" && caso.Ecra.ResoluçãoEcra.designação == "1280x720 (HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.08);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1280x720 (HD)" && caso.Ecra.ResoluçãoEcra.designação == "2560x1440 (2K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.08);

                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1280x720 (HD)" && caso.Ecra.ResoluçãoEcra.designação == "3840x2160 (4K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.12);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "3840x2160 (4K)" && caso.Ecra.ResoluçãoEcra.designação == "1280x720 (HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.12);
                //----------------
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "2560x1440 (2K)" && caso.Ecra.ResoluçãoEcra.designação == "1920x1080 (FULL HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.06);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1980x1080 (FULL HD)" && caso.Ecra.ResoluçãoEcra.designação == "2560x1440 (2K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.06);

                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "3840x2160 (4K)" && caso.Ecra.ResoluçãoEcra.designação == "1920x1080 (FULL HD)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.08);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "1980x1080 (FULL HD)" && caso.Ecra.ResoluçãoEcra.designação == "3840x2160 (4K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.08);
                //-----------------
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "3840x2160 (4K)" && caso.Ecra.ResoluçãoEcra.designação == "2560x1440 (2K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.04);
                if (DataBase.ResoluçãoEcras.Single(x => x.idResoluçãoEcra == e.idResoluçãoEcra).designação == "2560x1440 (2K)" && caso.Ecra.ResoluçãoEcra.designação == "3840x2160 (4K)")
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= (int)(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * 0.04);

            }

            if (e.tamanho > caso.Ecra.tamanho)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeTamanhoEcra(e.tamanho, caso.Ecra.tamanho)) * 0.025);
            }

            if (e.tamanho < caso.Ecra.tamanho)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeTamanhoEcra(e.tamanho, caso.Ecra.tamanho)) * 0.025);
            }

            if (p.velocidadeProcessador > caso.Processador.velocidadeProcessador)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeVelocidadeProcessador(p.velocidadeProcessador, caso.Processador.velocidadeProcessador)) * 0.05);
            }

            if (p.velocidadeProcessador < caso.Processador.velocidadeProcessador)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeVelocidadeProcessador(p.velocidadeProcessador, caso.Processador.velocidadeProcessador)) * 0.05);
            }

            if (p.nucleosProcessador > caso.Processador.nucleosProcessador)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeNucleosProcessador(p.nucleosProcessador, caso.Processador.nucleosProcessador)) * 0.025);
            }

            if (p.nucleosProcessador < caso.Processador.nucleosProcessador)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeNucleosProcessador(p.nucleosProcessador, caso.Processador.nucleosProcessador)) * 0.025);
            }

            if (c.resolucaoFrontal > caso.Camera.resolucaoFrontal)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeResolucaoCameraFrontal(c.resolucaoFrontal, caso.Camera.resolucaoFrontal)) * 0.05);
            }

            if (c.resolucaoFrontal < caso.Camera.resolucaoFrontal)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeResolucaoCameraFrontal(c.resolucaoFrontal, caso.Camera.resolucaoFrontal)) * 0.05);
            }

            if (c.resolucaoTraseira > caso.Camera.resolucaoTraseira)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeResolucaoCameraTraseira(c.resolucaoTraseira, caso.Camera.resolucaoTraseira)) * 0.05);
            }

            if (c.resolucaoTraseira < caso.Camera.resolucaoTraseira)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeResolucaoCameraTraseira(c.resolucaoTraseira, caso.Camera.resolucaoTraseira)) * 0.05);
            }

            if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação != caso.Estado.designação)
            {
                int e1 = 0, e2 = 0;
                if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação == "Péssimo") e1 = 0;
                if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação == "Mau") e1 = 1;
                if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação == "Intermédio") e1 = 2;
                if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação == "Bom") e1 = 3;
                if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação == "Muito Bom") e1 = 4;
                if (DataBase.Estados.Single(x => x.idEstado == tm.idEstado).designação == "Como Novo") e1 = 5;

                if (caso.Estado.designação == "Péssimo") e2 = 0;
                if (caso.Estado.designação == "Mau") e2 = 1;
                if (caso.Estado.designação == "Intermédio") e2 = 2;
                if (caso.Estado.designação == "Bom") e2 = 3;
                if (caso.Estado.designação == "Muito Bom") e2 = 4;
                if (caso.Estado.designação == "Como Novo") e2 = 5;

                int resultado = (5 - Math.Abs(e1 - e2)) / 5;
                if (e1 > e2) //impossivel ser igual, verificado antes
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * ((1 - resultado) * 0.1));
                else
                    DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * ((1 - resultado) * 0.1));
            }

            if (tm.idade > caso.idade)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal += Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeIdade(tm.idade, caso.idade)) * 0.1);
            }

            if (tm.idade < caso.idade)
            {
                DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal -= Convert.ToInt32(DataBase.Telemovels.Single(x => x.idTelemovel == idT).valorFinal * (1 - SimiliridadeIdade(tm.idade, caso.idade)) * 0.1);
            }
            DataBase.SubmitChanges();
        }
    }
}