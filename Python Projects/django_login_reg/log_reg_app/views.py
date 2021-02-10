from datetime import datetime
from .models import user, manager
from django.shortcuts import render, redirect, HttpResponse
from django.contrib import messages
import bcrypt


def index(request):
  return render(request, "index.html")


def success(request):
  if request.session.get('userid'):
    return render(request, "success.html")
  else:
    return HttpResponse('Duh you have to login')


def logout(request):
  del request.session['userid']
  return redirect('/')


def register(request):
  errors = user.objects.validator(request.POST)
  if len(errors) > 0:
    for key, value in errors.items():
      messages.error(request, value)
    return redirect('/')
  else:
    email = request.POST.get('email')
    first_name = request.POST.get('first_name')
    last_name = request.POST.get('last_name')
    password = request.POST['password']
    password_confirm = request.POST['password_confirm']
    birthday = request.POST['birthday']
    if len(password) < 8:
      return HttpResponse('Password must be longer than 8 characters')
    pw_hash = bcrypt.hashpw(password.encode(), bcrypt.gensalt()).decode()
    user.objects.create(email=request.POST['email'], password=pw_hash, first_name=first_name, last_name=last_name, birthday=birthday)
    person = user.objects.filter(email=request.POST['email'])
    if person:
      logged_user = person[0]
      request.session['userid'] = logged_user.id
      return redirect('/success')


def login(request):
  person = user.objects.filter(email=request.POST['email'])
  if person:
    logged_user = person[0]
    if bcrypt.checkpw(request.POST['password'].encode(), logged_user.password.encode()):
      request.session['userid'] = logged_user.id
    return redirect('/success')
  else:
    return redirect("/")


