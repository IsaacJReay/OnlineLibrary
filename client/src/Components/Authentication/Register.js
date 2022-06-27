import React, { useState, useCallback, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';

import Form, { Input } from '../Form';
import { 
  setRegisterValidationEmail,
  setRegisterValidationPasswordConfirmation,
  setRegisterValidationUsername,
  setRegisterValidationPassword,
  setRegisterButtonDisable
} from '../../store/action/authentication'

function Register(){;

  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [passwordConfirmation, setPasswordConfirmation] = useState('');
  const [invalidUsername, setInvalidUsername] = useState('');
  const [invalidEmail, setInvalidEmail] = useState('');
  const [invalidPassword, setInvalidPassword] = useState('');
  const [invalidPasswordConfirmation, setInvalidPasswordComfirmation] = useState('');
  const registerValidations = useSelector(state => state.authentication.registerValidations);
  const registerButtonDisable = useSelector(state => state.authentication.registerButtonDisable);
  const dispatch = useDispatch();

  useEffect(function(){
    const validations = [];

    for(let key in registerValidations){
      validations.push(registerValidations[key]);
    }

    const isValidate = validations.every(validation => validation === true);
    dispatch(setRegisterButtonDisable(!isValidate));
  }, [registerValidations, dispatch]);

  const handleUsernameChange = useCallback(function(event){
    setUsername(event.target.value);

    if(event.target.value === ''){
      setInvalidUsername('Username is required');
      dispatch(setRegisterValidationUsername(false));
    }else{
      setInvalidUsername('');
      dispatch(setRegisterValidationUsername(true));
    }
  }, [dispatch]);

  const handleEmailChange = useCallback(function(event){
    setEmail(event.target.value);

    if(event.target.value === ''){
      setInvalidEmail('Email is required');
      dispatch(setRegisterValidationEmail(false));
      return;
    }

    const regExp = /^([a-zA-Z0-9.\-_]+)@([a-zA-Z0-9]+).([a-z]{2,5})$/g;

    if(!regExp.test(event.target.value)){
      setInvalidEmail('Incorrect email');
      dispatch(setRegisterValidationEmail(false));
    }else{
      setInvalidEmail('');
      dispatch(setRegisterValidationEmail(true));
    }
  }, [dispatch]);

  const handlePasswordChange = useCallback(function(event){
    setPassword(event.target.value);

    if(event.target.value === ''){
      setInvalidPassword('Password is required');
      dispatch(setRegisterValidationPassword(false));
      return;
    }

    const regExp = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$/g;

    if(!regExp.test(event.target.value)){
      setInvalidPassword('Password needs to be 8 characters long, at least one small letter, one capital letter, one number and one symbol');
     dispatch(setRegisterValidationPassword(false));
    }else{
      setInvalidPassword('');
      dispatch(setRegisterValidationPassword(true));
    }


  }, [dispatch]);

  const handlePasswordConfirmationChange = useCallback(function(event){
    setPasswordConfirmation(event.target.value);

    if(event.target.value === ''){
      setInvalidPasswordComfirmation('Password confirmation is required');
      dispatch(setRegisterValidationPasswordConfirmation(false));
      return;
    }

    if(password !== event.target.value){
      setInvalidPasswordComfirmation('Password does not match');
      dispatch(setRegisterValidationPasswordConfirmation(false));
    }else{
      setInvalidPasswordComfirmation('');
      dispatch(setRegisterValidationPasswordConfirmation(true));
    }
  }, [password, dispatch]);

  const handleSubmit = useCallback(function(event){
    event.preventDefault();
  }, []);

  console.log(invalidUsername);

  return (
    <div>
      <Form 
        submitTitle="Register" 
        onSubmit={ handleSubmit }
        submitOptions={{
          disable: registerButtonDisable
        }}

      >
        <Input 
          label="Username" 
          value={ username } 
          onInputChange={ handleUsernameChange }
          invalidMessage={ invalidUsername }
          type="text"
          valid={ registerValidations.username }
        />
        <Input 
          label="Email" 
          value={ email } 
          onInputChange={ handleEmailChange }
          invalidMessage={ invalidEmail }
          type="email"
          valid={ registerValidations.email }
        />
        <Input 
          label="Password" 
          value={ password } 
          onInputChange={ handlePasswordChange }
          invalidMessage={ invalidPassword }
          type="password"
          valid={ registerValidations.password }
        />
        <Input 
          label="Password confirmation" 
          value={ passwordConfirmation } 
          onInputChange={ handlePasswordConfirmationChange }
          invalidMessage={ invalidPasswordConfirmation }
          type="password"
          valid={ registerValidations.passwordConfirmation }
        />
      </Form>
    </div>
  );
}

export default Register;