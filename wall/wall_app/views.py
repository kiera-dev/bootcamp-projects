import time
from datetime import datetime
from .models import User, Manager, Messages, Comments
from django.shortcuts import render, redirect, HttpResponse
from django.contrib import messages
from dateutil.relativedelta import relativedelta
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
  errors = User.objects.validator(request.POST)
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
    User.objects.create(email=request.POST['email'], password=pw_hash, first_name=first_name, last_name=last_name, birthday=birthday)
    person = User.objects.filter(email=request.POST['email'])
    if person:
      logged_user = person[0]
      request.session['userid'] = logged_user.id
      return redirect('/wall')


def login(request):
  person = User.objects.filter(email=request.POST['email'])
  if person:
    logged_user = person[0]
    if bcrypt.checkpw(request.POST['password'].encode(), logged_user.password.encode()):
      request.session['userid'] = logged_user.id
      print(request.session['userid'])
      print(logged_user.id)
    return redirect('/wall')
  else:
    return redirect("/")

def message(request):
  if request.session.get('userid'):
    user_id = User.objects.get(id=request.session.get('userid'))
    msg = request.POST.get('message')
    Messages.objects.create(msg_content=msg, user=user_id)
    return redirect('/wall')
  else:
    return HttpResponse('You need to be logged in to post.')

def comment(request, msg_id):
  if request.session.get('userid'):
    user_id = User.objects.get(id=request.session.get('userid'))
    comment = request.POST.get('comment')
    message = Messages.objects.get(id=msg_id)
    Comments.objects.create(comment_content=comment, user=user_id, message=message)
    return redirect('/wall')
  else:
    return HttpResponse('You need to be logged in to post.')

def wall(request):
  if request.session.get('userid'):
    user_id = request.session.get('userid')
    user_info = User.objects.get(id=user_id)
    messages = Messages.objects.all().order_by('-created_at')
    comment_info = Comments.objects.all()
    context = {
      'comment_info': comment_info,
      'user': user_info,
      'messages': messages,
      'user_id': user_id,
    }
    return render(request, "wall.html", context)
  else:
    return HttpResponse('You need to be logged in to view this page.')

def del_message(request, msg_id):
  if request.session.get('userid'):
    # user_info = User.objects.get(id=request.session.get('userid'))
    message_info = Messages.objects.get(id=msg_id)
    time_difference = (datetime.now().timestamp() - message_info.created_at.timestamp()) / 60
    print(time_difference)
    if message_info.user.id == request.session.get('userid') and time_difference < 30:
      delete_message = Messages.objects.get(id=msg_id)
      delete_message.delete()
      return redirect('/wall')
    else:
      return HttpResponse('You do not have permission to do that.')
  else:
    return HttpResponse('You do not have permission to do that.')

