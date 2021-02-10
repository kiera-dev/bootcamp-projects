import React from "react";

const Search = props => {
    const {submitHandler, searchTerms, changeHandler} = props;
    return(
        <div>
            <div>
            <label htmlFor="category">Search:</label>
            <select name="category">
                <option value="people">People</option>
                <option value="films">Films</option>
                <option value="starships">Starships</option>
                <option value="vehicles">Vehicles</option>
                <option value="species">Species</option>
                <option value="species">Planet</option>
            </select>
            </div>
            <div>
                <label htmlFor="id">Id:</label>
                <input type="number"></input>
            </div>
        </div>
    )
}

export default Search;