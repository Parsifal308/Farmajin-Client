public class TODOLIST
{
    //TODO: IMPLEMENT ENDPOINTS METHODS

    //After the endpoint testing is done, we should see what endpoints we need
    //and exactly what data and behaviour we need from them

    //METHODS ARE:

    // Login:
    //- Endpoint: http://3.83.143.130:3000/api/login // EXAMPLE
    //- Method: POST
    //- Request: User credentials
    /*- Example Body: 
    {
        "email": "Revosadassd@asdsa.com", //IMMUTABLE, MANDATORY
        "password": "soyadmin", //MANDATORY
    */
    //- Response: 
    /*
    {
        "Code": 0,
        "Token": "ASDASFASDA"
        "Name": "Alberto",
    }*/




    // Get Worlds: 
    //- Endpoint: http://localhost:8080/api/worlds // EXAMPLE
    //- Method: GET
    //- Request: A header with the session token. Session token is the token received when logging in
    //- Response: All data entries from Worlds in a JSON array
    /* "worlds": [
    {
        "Name": "MUNDO FARMA",
        "Description": "Ipsum asd asd asd adsa",
        "MainImage": "http://localhost:8080/api/worlds/{id}/mainImage.jpeg",
        "BackgroundImage": "http://localhost:8080/api/worlds/{id}/backgroundImage.jpeg",
    },
    ...
    */



    // Get Levels: 
    //- Endpoint: http://localhost:8080/api/levels // EXAMPLE
    //- Method: GET
    //- Request: A header with the session token. Session token is the token received when logging in
    //- Response: All data entries from Levels in a JSON array
    /* "levels": [
    {
        "Name": "Nuevo nivel",
        "MainImage": "http://localhost:8080/api/worlds/{id}/mainImage.jpeg",
        "World": "MUNDO FARMA",
        "StagesAmount": 3,
    },
    ...
    */




    // Get Videos: 
    //- Endpoint: http://localhost:8080/api/videos // EXAMPLE
    //- Method: GET
    //- Request: A header with the session token. Session token is the token received when logging in
    //- Response: All data entries from Videos in a JSON array
    /* "videos": [
    {
        "Title": "Nuevo nivel",
        "Description": "ASDSADSADSADAA",
        "Url": "https://youtube.com/asdasdsadsadasd",
        "World": "MUNDO FARMA",
        "Level": "Nuevo nivel",
        "Stage": 2,
    },
    ...
    */




    // Get Games: 
    //- Endpoint: http://localhost:8080/api/games // EXAMPLE
    //- Method: GET
    //- Request: A header with the session token. Session token is the token received when logging in
    //- Response: All data entries from Games in a JSON array
    /* "games": [
    {
        "Name": "Nuevo nivel",
        "MiniGame": "TapColor",
        "World": "MUNDO FARMA",
        "Level": "Nuevo nivel",
        "Stage": 1,
    },
    ...
    */


    //The client should only have the methods that are needed for the game to work

}
