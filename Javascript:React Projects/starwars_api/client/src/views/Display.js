import React, {useState,useEffect} from "react";
import axios from "axios"
import Droids from "../components/Droids"
import Person from "../components/Person"


const Display = props => {
    const {category, id} = props;
    const [error, setError] = useState(false);
    const [data,setData] = useState({})
    const {submitHandler, searchTerms, changeHandler} = props;
    useEffect(() => {
        axios.get(`https://swapi.dev/api/${category}/${id}`)
            .then(response =>{
                setData(response.data);
                setError(false);
            })
            .then(response => {
                setData({
                    ...data,
                    homew
            }
                )
    }
    return(
        <div>
            {
                error?
                <Droids/>
                : category.toLowerCase() == "people"
            }
        </div>
    )
}

export default Display;