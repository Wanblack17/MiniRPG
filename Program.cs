using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static inimigo;

namespace udemy01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Random rnd = new Random(); //Criamos a instancia no objeto Random, responsavem por gerar numeros aleatorios
            Mensagem("Escolha o nome do seu personagem: ");
            string Nome = Console.ReadLine();
            Personagem personagem1 = new Personagem();
            personagem1.setNomePersonagem(Nome);  //Aqui instaciamos o objeto Personagem, com a variavel nome digitada pelo usuario


            Classejogador jogador = new Classejogador();//Criacão de classe, a classe influencia os atributos do personagem tal como vida, esquiva e dano
            Mensagem("Qual será a classe dele? \nOpções \n1.Ladino \n2.Guerreiro");//Por enquantos temos as classes Ladino e Guerreiro
            Console.Write("Classe: ");
            string classedopersonagem = (Console.ReadLine()).ToLower();
            while (classedopersonagem != "ladino" && classedopersonagem != "guerreiro" && classedopersonagem != "1" && classedopersonagem != "2")
            {
                Mensagem("Classe Invalida, voce deve escolher entre Ladino e Guerreiro!!"); //Repetidor que certifica que o usuario escolha a classe correta
                classedopersonagem = (Console.ReadLine()).ToLower();
            }
            if (classedopersonagem == "ladino" || classedopersonagem == "1")
            {

                jogador.Nomeclasse = "Ladino"; //construtor da classe com base na resposta do usuario
                jogador.HpClasse = 120;
                jogador.ataqueMinimo = 2;
                jogador.ataqueMaximo = 43;
                jogador.esquivaMinima = 3;
                jogador.esquivaMaxima = 30;
                Mensagem($"Seu personagem se chama {personagem1.getNomePersonagem()}, ele tem {jogador.HpClasse} de vida, e ele será um {jogador.Nomeclasse}.");

            }
            else if (classedopersonagem == "guerreiro" || classedopersonagem == "2")
            {

                jogador.Nomeclasse = "Guerreiro";
                jogador.HpClasse = 150;
                jogador.ataqueMinimo = 5;
                jogador.ataqueMaximo = 60;
                jogador.esquivaMinima = 0;
                jogador.esquivaMaxima = 15;
                Mensagem($"Seu personagem se chama {personagem1.getNomePersonagem()}, ele tem {jogador.HpClasse} de vida, e ele será um {jogador.Nomeclasse}.");

            }


            //Aqui inicializamos três objetos, inimigo, cura e esquiva. com métodos e atributos propios que serão usados durante a batalha

            inimigo inimigo1 = new inimigo();
            inimigo1.nome = "Godric";
            inimigo1.hp = 100;
            inimigo1.esquivaMinima = 0;
            inimigo1.esquivaMaxima = 15;

            Buffs buff1 = new Buffs();
            buff1.nome = "Cura";
            buff1.buffMinimo = (jogador.HpClasse / 4);
            buff1.buffMaximo = (jogador.HpClasse / 2);


            Buffs buff2 = new Buffs();
            buff2.nome = "Esquiva";
            buff2.buffMinimo = (jogador.esquivaMaxima / 4);
            buff2.buffMaximo = (jogador.esquivaMaxima / 2);
            buff2.UsarBuff(jogador.esquivaMaxima, rnd);



            Mensagem($"Seu Inimigo {inimigo1.nome}, está diante de voce!!!");
            while (jogador.HpClasse > 0 && inimigo1.hp > 0) //Aqui começa a batalha por turno. Só acaba quando um dos 2 morrer
            {

                int dano1 = 0;
                int esquivaInimigo = 0;

                Mensagem("O que deseja fazer agora? ");
                Mensagem("1.Atacar");
                Mensagem("2.Poção de Cura");

                Mensagem("3.Poção de Esquiva");
                string acao = Console.ReadLine(); //A escolha da ação, podendo ser ataque, poção de cura e poção de esquiva
                if (acao == "1") //a condição executa um codigo com base na escolha do jogador
                    

                {
                    dano1 = jogador.GerarAtaque(rnd);
                    esquivaInimigo = inimigo1.GerarEsquivaInimigo(rnd); //no caso de ataque, duas variaveis geram um numero aleatorio (dano e esquiva)
                    if (esquivaInimigo < dano1) //Se a esquiva for menor que o dano, o inimigo recebe o dano
                    {
                        inimigo1.hp = personagem1.Personagematacar(inimigo1.hp, dano1);
                        Mensagem($"Ataque: {dano1}\nSeu inimigo está com {inimigo1.hp} de vida");
                        if (inimigo1.hp <= 0) break; //aqui é mostrado quanto foi o dano, a vida atualizada
                    }
                    else
                    {
                        Mensagem("Seu inimigo conseguiu se esquivar");
                    }
                }
                    else if (acao == "2")
                {
                    jogador.HpClasse = buff1.UsarBuff(jogador.HpClasse, rnd);
                    Mensagem($"Você usou a poção de Cura, agora sua vida é {jogador.HpClasse}!!!");
                }
                else if (acao == "3")
                {
                    jogador.esquivaMaxima = buff2.UsarBuff(jogador.esquivaMaxima, rnd);
                    Mensagem($"Você usou a poção de Esquiva, agora suas chances de esquivar aumentaram!!!");
                }

                //as ações 2 e 3 usam métodos que aumetam atributos do personagem, mas isso pula a vez do turno dele





                //agora é a vez do inimigo

                int danoIninmigo = (rnd.Next(0, 75));
                int esquiva = jogador.GerarEsquiva(rnd);
               Mensagem("O inimigo te atacou!");
                if (esquiva >= danoIninmigo)
                {
                    Console.WriteLine("Você conseguiu se esquivar!!"); //assim como o inimigo, seu personagem também se esquiva e lógica segue a mesma, a esquiva deve ser maior que o dano
                }
                else
                {
                    jogador.AtualizarHp(danoIninmigo);
                    Mensagem($"O Inimigo deu um ataque de {danoIninmigo} \nVida:{jogador.HpClasse}");
                    if (jogador.HpClasse <= 0)
                    {
                        Mensagem("Você perdeu!!!");
                    }
                }

            }

        }
     
        public static void Mensagem(string texto)
        {
            Console.WriteLine(texto);
        }

    } }
    public class Personagem
    {
        private string nomePersonagem;




        public void setNomePersonagem(string nomePersonagem)
        {
            this.nomePersonagem = nomePersonagem;
        }
        public string getNomePersonagem() { return this.nomePersonagem; }

       
        public int Personagematacar(int vida, int dano)
        {
            int vidarestante = vida - dano;
            if (vidarestante <= 0)
            {
                Console.WriteLine("Você matou o inimigo");
                return 0;

            }
            return vidarestante;
        }
    }


public class inimigo
{
    public string nome { get; set; }
    public int hp { get; set; }
    public int esquivaMinima;
    public int esquivaMaxima;



    public int atacar(int vida, int ataque)
    {
        int vidarestante = vida - ataque;
        if (vidarestante <= 0)
        {
            Console.WriteLine("O Inimigo te derrotou");
            return 0;
        }
        return vidarestante;

    }
    public int GerarEsquivaInimigo(Random rnd)
    {

        return rnd.Next(esquivaMinima, esquivaMaxima + 1);
    }
}


    public class Classejogador
    {
        public string Nomeclasse;
        public int HpClasse;
        public int ataqueMinimo;
        public int ataqueMaximo;
        public int esquivaMinima;
        public int esquivaMaxima;

        public int GerarAtaque(Random rnd)
        {

            return rnd.Next(ataqueMinimo, ataqueMaximo + 1);
        }

        public int GerarEsquiva(Random rnd)
        {

            return rnd.Next(esquivaMinima, esquivaMaxima + 1);
        }

        public void AtualizarHp(int dano)
        {
            HpClasse -= dano;
            if (HpClasse <= 0) { HpClasse = 0; }
            ;
        }

    } 


public class Buffs
{
    public string nome;
    public int buffMinimo;
    public int buffMaximo;

    public int UsarBuff(int vida,Random rnd)
    {
        int buff = rnd.Next(this.buffMinimo, this.buffMaximo + 1);
        return vida + buff;
    }


}

