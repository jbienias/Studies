require 'hello_world'

describe "#hello" do
  it "returns 'Hello, World!'" do
    expect(HelloWorld.hello).to eq 'Hello, World!'
  end
end
