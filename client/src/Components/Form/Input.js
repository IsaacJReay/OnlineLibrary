import React, { useRef, useEffect  } from 'react';
import PropTypes from 'prop-types'

const propTypes = {
  label: PropTypes.string,
  value: PropTypes.string,
  onInputChange: PropTypes.func,
  invalidMessage: PropTypes.string,
  type: PropTypes.string,
  placeholder: PropTypes.string,
  valid: PropTypes.bool
};

function Input({ label, value, onInputChange, invalidMessage, type, placeholder, valid }){

  const inputRef = useRef(null);

  useEffect(function(){
    if(invalidMessage === ''){
      inputRef.current.style.borderBottom = '2px solid #000';
    }else{
      inputRef.current.style.borderBottom = '2px solid #f50707';
    }

    if(valid){
      inputRef.current.style.borderBottom = '2px solid #188750';
    }
  }, [invalidMessage, valid]);

  const invalidMessageMarkup = !!invalidMessage && (
    <small style={ styles.invalidMessage }>
      { invalidMessage }
    </small>
  );

  return (
    <div style={ styles.input }>
      <label style={ styles.label }>{ label }</label>
      <div style={{ display: 'flex', flexDirection: 'column' }}>
        <input 
          style={styles.inputField } 
          value={ value }
          onChange={ onInputChange }
          type={ type }
          placeholder={ placeholder }
          ref={ inputRef }
        />
        { invalidMessageMarkup }
      </div>
    </div>
  );
}

Input.propTypes = propTypes;

const styles = {
  input: {
    display: 'flex',
    flexDirection: 'column',
    gap:'5px'
  },
  label: {
    fontSize: '1rem',
    fontWeight: '500'
  },
  inputField: {
    padding: '0.5rem 1.2rem',
    fontSize: '1rem',
    border: 'none',
    backgroundColor: '#f2f4f1'
  },
  invalidMessage: {
    color: '#f50707'
  }
};

export default Input;