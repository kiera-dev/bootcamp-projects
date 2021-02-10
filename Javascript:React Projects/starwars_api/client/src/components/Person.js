import React,{useState, useEffect}  from "react"
import axios from "axios"


const Person = props =>{
const {name, height, mass, hair_color, skin_color} = props.person;
return(
    <div>
        <h3>{name}</h3>
        <ul>
            <li>Height: {height}</li>
            <li>Weight: {mass}</li>
            <li>Hair: {hair_color}</li>
            <li>Skin: {skin_color}</li>
        </ul>
    </div>
)

}



export default Person;