import React, { useState, useCallback, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import Form, { Input } from '../Form';
import { 
  setLoginValidationEmail, 
  setLoginValidationPassword,
  setLoginButtonDisable 
} from '../../store/action/authentication';

function Login(){

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [emailInvalidMessage, setEmailInvalidMessage] = useState('');
  const [passwordInvalidMessage, setPasswordInvalidMessage] = useState('');
  const loginValidations = useSelector(state => state.authentication.loginValidations);
  const loginButtonDisable = useSelector(state => state.authentication.loginButtonDisable);
  const dispatch = useDispatch();

  useEffect(function(){
    const validations = [];

    for(let key in loginValidations){
      validations.push(loginValidations[key]);
    }

    const isValidate = validations.every(validation => validation === true);

    dispatch(setLoginButtonDisable(!isValidate));
  }, [loginValidations, dispatch]);

  const handleEmailChange = useCallback(function(event){
    setEmail(event.target.value);

    if(event.target.value === ''){
      setEmailInvalidMessage('Email is required');
      dispatch(setLoginValidationEmail(false));
      return;
    }

    const regExp = /^([a-zA-Z0-9]+)\@([a-zA-Z0-9]+)\.([a-z]{2,5})$/g;

    if(!regExp.test(event.target.value)){
      setEmailInvalidMessage('Incorrect email');
      dispatch(setLoginValidationEmail(false));
    }else{
      setEmailInvalidMessage('');
      dispatch(setLoginValidationEmail(true));
    }

  }, [dispatch]);

  const handlePasswordChange = useCallback(function(event){
    setPassword(event.target.value);
    if(event.target.value === ''){
      setPasswordInvalidMessage('Password is required');
      dispatch(setLoginValidationPassword(false));
    }else{
      setPasswordInvalidMessage('');
      dispatch(setLoginValidationPassword(true));
    }
  }, [dispatch]);

  const handleSubmit = useCallback(function(event){
    event.preventDefault();
  }, []);

  return (
    <div>
      <Form 
        submitTitle="Login" 
        onSubmit={ handleSubmit } 
        submitOptions={{
          disable: loginButtonDisable
        }}
      >
        <Input 
          label="Email" 
          value={ email } 
          onInputChange={ handleEmailChange }
          type="email"
          invalidMessage={ emailInvalidMessage }
          placeholder="Email"
          valid={ loginValidations.email }
        />
        <Input 
          label="Password" 
          value={ password } 
          onInputChange={ handlePasswordChange }
          invalidMessage={ passwordInvalidMessage }
          type="password"
          placeholder="Password"
          valid={ loginValidations.password }
        />
      </Form>
    </div>
  );
}

export default Login;