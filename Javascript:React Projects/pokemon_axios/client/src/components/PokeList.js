import React, {useState} from "react";
import axios from 'axios';

const PokeList = (props) => {
    const [pokemon, setPokemon] = useState("");
    const fetchPokemon = (event) => {
        axios.get("https://pokeapi.co/api/v2/pokemon?limit=807&offset=0")
            // .then((response) => {return response.json();})
            .then((response) => {
                // console.log(response)
                setPokemon(response.data.results)}).catch((error) => {console.log(error);
                    //ok well that was easier than i thought it was going to be O_o
            });
    }
    const alolaPokedex =()=> {

        if(pokemon === ""){
            return(
                <p>Push the button...</p>
            );
        }
        else{
            return (
                pokemon.map((pokemon, p)=> {
                return(
                    <li key={p}>{pokemon.name}</li>
                );
            }));
        }
    }

    return(
        <div>
            <button onClick ={fetchPokemon}>View Pokedex</button>
            <ul>
                {alolaPokedex()}
            </ul>
        </div>
    );
}

export default PokeList;
