# Projet-Web

Theme : Un site de réservation de voyages spatiaux.

Description : Nous souhaitons réaliser un site qui permet à un utilisateur de réserver une place dans une navette spatiale, vers une planète au choix, en fonction des voyages planifiés.

Rèlges: Code en Anglais (Nom des variables; méthodes ...)

Fonctionnalités : Must-have : 

- User registration (with authentification) => Voir sous-fonctionnalités obligatoires énoncé.
- Système de réservation.
- Système de gestion et planificatoin des voyages.


Fonctionnalités : Nice to have :
- Système de fidelity card.
- Système de réservation visuelle des places (inspiration : Kinepolis) https://tickets.kinepolis.be/Booking/Seating


Agenda :
Pour le Lundi 18 18h:
Réfléchir au Schéma EA. + Faire une db sur azure.



#RetryPolicy :
We'll be using Polly Nuget in order to implement all our retry policies :

1 - We install the following nuget :
    Microsoft.Extensions.Http.Polly (this will contain all polly's dependencies)
2 - We'll be implementing an Interface which will contain all the policies signatures.
3 - Service that implements this Interface
4 - We inject the dependency <Interface, Service>
5 - We add HttpClient, we give it a name and add to it PolicyHandler extension method that defines which policy to use.
6 - Finally, in the controller, we'll be using IHttpClientFactory to call the previous service injected with the given name.

#MiddleWare Nuget
We can for example create a simple dedicated nuget that handles LoggerBehaviours using MediatR


#Api Consume 

We could for example consume an api that retrieves the list of airports by IATA code

Link : https://rapidapi.com/Active-api/api/airport-info

Code C# to be improved :

var client = new HttpClient();
var request = new HttpRequestMessage
{
	Method = HttpMethod.Get,
	RequestUri = new Uri("https://airport-info.p.rapidapi.com/airport?iata=JFK"),
	Headers =
	{
		{ "X-RapidAPI-Host", "airport-info.p.rapidapi.com" },
		{ "X-RapidAPI-Key", "3398f4ab61msh2fec552303aed05p1babbajsn4969aa1f509b" },
	},
};
using (var response = await client.SendAsync(request))
{
	response.EnsureSuccessStatusCode();
	var body = await response.Content.ReadAsStringAsync();
	Console.WriteLine(body);
}



