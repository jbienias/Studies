# Animal Crazy
## Projekt "Database"

| Prowadzący | Studenci |
:--:|:--:
| [dr Włodzimierz Bzyl](https://github.com/wbzyl) |  [Monika Barzowska](https://github.com/mbarzowska), [Jędrzej Dembowski](https://github.com/jdemb), [Jan Bienias](https://github.com/jbienias) |

#### Krótki opis projektu :

Konsolowa aplikacja w Ruby pozwalająca na dodawanie, usuwanie, listowanie, edytowanie
użytkowników w bazie danych zlokalizowanej w pliku.

#### Mocking Hell
1. double() -> user_spec.rb #delete_user, #add_user
```ruby
let(:file) { double('file') }

expect(File).to receive(:open).with(path, 'a').and_yield(file)
expect(file).to receive(:write).with(line)
test_user.add_user(path)
```
2. StringIO -> user_spec.rb '#delete_user
```ruby
test_io = StringIO.new
user.write_data(test_io, path)
expect(test_io.string.lines.count).to eq(size - 1)
```
3. FakeFS -> user_spec.rb #write_data, #delete_user
```ruby
FakeFS do
  tmp = File.open(path1, 'w')
  File.open(path2, 'w').write(word)
  expect { user.write_data(tmpFile, path2) }.to_not raise_error
end
```
