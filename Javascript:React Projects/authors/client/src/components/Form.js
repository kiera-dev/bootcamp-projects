import React from 'react'
import { Link } from '@reach/router';

const Form = props => {
    const { author, errors, changeHandler,submitHandler, action } = props;


    return (
        <form onSubmit={submitHandler}>
            <div className="name-entry">
                <label htmlFor="name">Name: </label>
                <input id="name-input" type="text" name="name"  onChange={changeHandler} value={author.name}/>
                <p id ="name-error"></p>
                <br></br>
                {
                    errors.name ?
                    <span className="error-text">{errors.name.message}</span>
                    : ''
                }
            </div>
           <Link to="/"><button>Cancel</button></Link>
            <button type="submit" value={action}>Submit</button>
        </form>
    );
}

export default Form;