import React from 'react'

const ProdForm = props => {
    const { product, errors, changeHandler,submitHandler, action } = props;


    return (
        <form onSubmit={submitHandler}>
            <div className="name-entry">
                <label htmlFor="name">Name: </label>
                <input id="name-input" type="text" name="name"  onChange={changeHandler} value={product.name}/>
                <p id ="name-error"></p>
                <br></br>
                {
                    errors.name ?
                    <span className="error-text">{errors.name.message}</span>
                    : ''
                }
            </div>


            <div className="price-entry">
                <label htmlFor="price">Price: </label>
                <input id="price-input" type="number" name="price"  onChange={changeHandler} value={product.price}/>
                <p id ="price-error"></p>
                <br></br>
                {
                    errors.price ?
                    <span className="error-text">{errors.price.message}</span>
                    : 
                    ''
                }
            </div>


            <div className="desc-entry">
                <label htmlFor="desc">Description: </label>
                <input id = "desc-input" type="text" name="desc" onChange={changeHandler} value={product.desc}/>
                <p id ="desc-error"></p>
                <br></br>
                {
                    errors.desc ?
                    <span className="error-text">{errors.desc.message}</span>
                    : ''
                }
            </div>
            <input type="submit" value={action}/>
        </form>
    );
}

export default ProdForm;