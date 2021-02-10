from datetime import datetime
from .models import show
from django.shortcuts import render, redirect, HttpResponse


def index(request):
  return redirect('/shows')


def shows(request):
  context = {
    'show_info': show.objects.all(),
  }
  return render(request, "shows.html", context)


def edit(request, id_number):
  context = {
    'show_info': show.objects.all(),
    'requested': show.objects.get(id=id_number)
  }
  return render(request, "edit.html", context)


def add(request):
  return render(request, "add.html")


def show_info(request, id_number):
  id_number = int(id_number)
  context = {
    'show_info': show.objects.all(),
    'requested': show.objects.get(id=id_number)
  }
  return render(request, "show_info.html", context)


def add_show(request):
  title_from_form = request.POST.get('title')
  desc_from_form = request.POST.get('desc')
  network_from_form = request.POST.get('network')
  release_from_form = request.POST.get('release_date')
  release_time = datetime.strptime(release_from_form, '%Y-%m-%d')
  if len(title_from_form) < 2:
    return HttpResponse('Data validation error. Title must be longer than 2 characters.')
  elif len(desc_from_form) < 10:
    return HttpResponse('Data validation error. Description must be 10 characters or longer.')
  elif len(network_from_form) < 3:
    return HttpResponse('Data validation error. Network must be 3 characters or longer.')
  elif release_time > datetime.now():
    return HttpResponse("Release date can't be in the future.")
  else:
    show.objects.create(title = title_from_form, network = network_from_form, release_date=release_from_form, desc = desc_from_form)
    last_show = show.objects.last()
    id_number = last_show.id

    return redirect(f'/show_info/{id_number}')


def show_edit(request):
  print(request.POST.get('release_date'))
  id_number = request.POST.get('id_number')
  edit_show = show.objects.get(id=id_number)
  edit_show.title = request.POST.get('title')
  edit_show.desc = request.POST.get('desc')
  edit_show.network = request.POST.get('network')
  edit_show.release_date = request.POST.get('release_date')
  release_time = datetime.strptime(edit_show.release_date, '%Y-%m-%d')
  if len(edit_show.title) < 2:
    return HttpResponse('Data validation error. Title must be longer than 2 characters.')
  elif len(edit_show.desc) < 10:
    return HttpResponse('Data validation error. Description must be 10 characters or longer.')
  elif len(edit_show.network) < 3:
    return HttpResponse('Data validation error. Network must be 3 characters or longer.')
  elif release_time > datetime.now():
    return HttpResponse("Release date can't be in the future.")
  else: 
    edit_show.save()
    return redirect(f'/show_info/{id_number}')


def delete(request, id_number):
  delete_show = show.objects.get(id=id_number)
  delete_show.delete()
  return redirect('/shows')

