from django.db import models
import datetime 
    
class show(models.Model):
    title = models.CharField(max_length=255, blank=False, unique=True)
    network = models.CharField(max_length=255, blank=False)
    desc = models.CharField(max_length=255)
    release_date = models.DateField(null=True)
    created_at = models.DateTimeField(auto_now_add=True)
    updated_at = models.DateTimeField(auto_now=True)


