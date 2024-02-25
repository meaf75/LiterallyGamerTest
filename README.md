# Live demo

Puedes probar el proyecto con esta url (Usar navegador con la opcion "Hardware acceleration" activada es recomendado): [https://literallygamer.web.app/](https://literallygamer.web.app/)

	This repository is using Git lfs for binary files
	Unity version: 2020.3.14f1

# Detalles

se desarrollará un juego basico en Unity3D haciendo uso del material adjunto, el juego se dividirá en dos instancias principales.

- [x] al iniciar, se desplegará una interfaz en canvas que nos mostrará el modelo, esta incluirá 4 botones, 3 de ellos al ser clickeados el personaje cambiara su animacion por la asignada a cada botón, mientras el ultimo
determinara la aniamcion a seleccionar, al terminar este proceso se enviará al usuario a una segunda escena.

	![main_menu](https://firebasestorage.googleapis.com/v0/b/meaf75-portfolio.appspot.com/o/litgam-main_menu.gif?alt=media&token=2d700fce-1634-4e4f-bbb2-6e5aadf619eb)

- [x] en esta segunda fase la vista será en primera persona, se podra ver al personaje reproduciendo la animacion asignada en una pequeña esquina en el ui.
por otro lado el usuario podra escoger entre 3 armas disintas en el suelo, cada una de ellas disparará de una forma distinta.

- [x] la primera realizará un disparo con trayectoria parabolica.

	![weapon_1](https://firebasestorage.googleapis.com/v0/b/meaf75-portfolio.appspot.com/o/litgam-parabolic.gif?alt=media&token=ef37d0ec-f618-482d-bab4-150cd55a6353)

- [x] la segunda generará un campo alrdedor donde atraerá objetos cercanos haciendolos orbitar alrededor del proyectil.

	![weapon_2](https://firebasestorage.googleapis.com/v0/b/meaf75-portfolio.appspot.com/o/litgam-blackhole.gif?alt=media&token=f55c82d3-0813-4c04-8baf-106cc6e1dc29)

- [x] la tercera será libre pero deberá incluir alguna propiedad fisica similar a las anteriores descritas.

	![weapon_3](https://firebasestorage.googleapis.com/v0/b/meaf75-portfolio.appspot.com/o/litgam-free.gif?alt=media&token=4c7313d6-1140-4dbb-bc6c-76e11e3c3438)
